
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员                                                    
*│　作    者：yilezhu                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2018-12-31 16:43:28                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Services                                  
*│　类    名： ManagerService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using AutoMapper;
using Czar.Cms.Core.Extensions;
using Czar.Cms.Core.Helper;
using Czar.Cms.IRepository;
using Czar.Cms.IServices;
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _repository;
        private readonly IManagerRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IManagerLogRepository _managerLogRepository;

        public ManagerService(IManagerRepository repository, IManagerRoleRepository roleRepository, IMapper mapper, IManagerLogRepository managerLogRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _managerLogRepository = managerLogRepository;
        }

        public async Task<BaseResult> AddOrModifyAsync(ManagerAddOrModifyModel item)
        {
            var result = new BaseResult();
            Manager manager;
            if (item.Id == 0)
            {
                //TODO ADD
                manager = _mapper.Map<Manager>(item);
                manager.Password = AESEncryptHelper.Encode(CzarCmsKeys.DefaultPassword, CzarCmsKeys.AesEncryptKeys);
                manager.LoginCount = 0;
                manager.AddManagerId = 1;
                manager.IsDelete = false;
                manager.AddTime = DateTime.Now;
                if ( await _repository.InsertAsync(manager) > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }
            }
            else
            {
                //TODO Modify
                manager = await _repository.GetAsync(item.Id);
                if (manager != null)
                {
                    _mapper.Map(item, manager);
                    manager.ModifyManagerId = 1;
                    manager.ModifyTime = DateTime.Now;
                    if (_repository.Update(manager) > 0)
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                    }
                    else
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                    }
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonFailNoDataCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonFailNoDataMsg;
                }
            }
            return result;
        }

        public async Task<BaseResult> DeleteIdsAsync(int[] Ids)
        {
            var result = new BaseResult();
            if (Ids.Count() == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;

            }
            else
            {
                var count = await _repository.DeleteLogicalAsync(Ids);
                if (count > 0)
                {
                    //成功
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    //失败
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }


            }
            return result;
        }

        public async Task<TableDataModel> LoadDataAsync(ManagerRequestModel model)
        {
            string conditions = "where IsDelete=0 ";//未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"and (UserName like '%@Key%' or NickName like '%@Key%' or Remark like '%@Key%' or Mobile like '%@Key%' or Email like '%@Key%')";
            }
            var list = (await _repository.GetListPagedAsync(model.Page, model.Limit, conditions, "Id desc", model)).ToList();
            var viewList = new List<ManagerListModel>();
            list?.ForEach(x =>
            {
                var item = _mapper.Map<ManagerListModel>(x);
                item.RoleName = _roleRepository.GetNameById(x.RoleId);
                viewList.Add(item);
            });
            return new TableDataModel
            {
                count = await _repository.RecordCountAsync(conditions),
                data = viewList,
            };
        }

        public async Task<BaseResult> ChangeLockStatusAsync(ChangeStatusModel model)
        {
            var result = new BaseResult();
            //判断状态是否发生变化，没有则修改，有则返回状态已变化无法更改状态的提示
            var isLock = await _repository.GetLockStatusByIdAsync(model.Id);
            if (isLock == !model.Status)
            {
                var count = await _repository.ChangeLockStatusByIdAsync(model.Id, model.Status);
                if (count > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonDataStatusChangeCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonDataStatusChangeMsg;
            }
            return result;
        }

        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns>状态</returns>
        public async Task<Manager> SignInAsync(LoginModel model)
        {
            model.Password = AESEncryptHelper.Encode(model.Password.Trim(), CzarCmsKeys.AesEncryptKeys);
            model.UserName = model.UserName.Trim();
            string conditions = $"select * from {nameof(Manager)} where IsDelete=0 ";//未删除的
            conditions += $"and (UserName = @UserName or Mobile =@UserName or Email =@UserName) and Password=@Password";
            var manager = await _repository.GetAsync(conditions, model);
            if (manager != null)
            {
                manager.LoginLastIp = model.Ip;
                manager.LoginCount += 1;
                manager.LoginLastTime = DateTime.Now;
                _repository.Update(manager);
               await _managerLogRepository.InsertAsync(new ManagerLog()
                {
                    ActionType = CzarCmsEnums.ActionEnum.SignIn.ToString(),
                    AddManageId = manager.Id,
                    AddManagerNickName = manager.NickName,
                    AddTime = DateTime.Now,
                    AddIp = model.Ip,
                    Remark = "用户登录"
                });
            }
            return manager;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">修改密码实体</param>
        /// <returns>结果</returns>
        public async Task<BaseResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            BaseResult result = new BaseResult();
            string oldPwd = await _repository.GetPasswordByIdAsync(model.Id);//数据库中的密码
            if (oldPwd == AESEncryptHelper.Encode(model.OldPassword, CzarCmsKeys.AesEncryptKeys))
            {
                var count = await _repository.ChangePasswordByIdAsync(model.Id, AESEncryptHelper.Encode(model.NewPassword.Trim(), CzarCmsKeys.AesEncryptKeys));
                if (count > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.PasswordOldErrorCode;
                result.ResultMsg = ResultCodeAddMsgKeys.PasswordOldErrorMsg;
            }
            return result;
        }

        public async Task<Manager> GetManagerByIdAsync(int id)
        {

            return await _repository.GetAsync(id);
        }

        public async Task<Manager> GetManagerContainRoleNameByIdAsync(int id)
        {
            return await _repository.GetManagerContainRoleNameByIdAsync(id);
        }

        /// <summary>
        /// 个人资料修改
        /// </summary>
        /// <param name="model">个人资料修改实体</param>
        /// <returns>结果</returns>
        public async Task<BaseResult> UpdateManagerInfoAsync(ChangeInfoModel model)
        {
            BaseResult result = new BaseResult();
            //TODO Modify
            var manager = await _repository.GetAsync(model.Id);
            if (manager != null)
            {
                _mapper.Map(model, manager);
                if (await _repository.UpdateAsync(manager) > 0)
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                }
                else
                {
                    result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                    result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                }
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonFailNoDataCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonFailNoDataMsg;
            }
            return result;
        }
    }
}