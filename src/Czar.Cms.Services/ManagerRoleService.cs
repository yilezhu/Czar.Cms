
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员角色                                                    
*│　作    者：yilezhu                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2018-12-31 16:43:28                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Services                                  
*│　类    名： ManagerRoleService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using AutoMapper;
using Czar.Cms.Core.Extensions;
using Czar.Cms.Core.Models;
using Czar.Cms.IRepository;
using Czar.Cms.IServices;
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Services
{
    public class ManagerRoleService : IManagerRoleService
    {
        private readonly IManagerRoleRepository _repository;
        private readonly IMapper _mapper;

        public ManagerRoleService(IManagerRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        public async Task<BaseResult> AddOrModifyAsync(ManagerRoleAddOrModifyModel item)
        {
            var result = new BaseResult();
            ManagerRole managerRole;
            if (item.Id == 0)
            {
                //TODO ADD
                managerRole = _mapper.Map<ManagerRole>(item);
                managerRole.AddManagerId = 1;
                managerRole.IsDelete = false;
                managerRole.AddTime = DateTime.Now;
                if (await _repository.InsertByTransAsync(managerRole) > 0)
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
                managerRole = await _repository.GetAsync(item.Id);
                if (managerRole != null)
                {
                    _mapper.Map(item, managerRole);
                    managerRole.ModifyManagerId = 1;
                    managerRole.ModifyTime = DateTime.Now;
                    if (await _repository.UpdateByTransAsync(managerRole) > 0)
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

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleId">角色主键id</param>
        /// <returns>结果实体</returns>
        public async Task<BaseResult> DeleteIdsAsync(int[] roleId)
        {
            var result = new BaseResult();
            if (roleId.Count() == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;

            }
            else
            {
                var count = await _repository.DeleteLogicalAsync(roleId);
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

        public async Task<List<ManagerRole>> GetListByConditionAsync(ManagerRoleRequestModel model)
        {
            string conditions = "where IsDelete=0 ";//未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"and RoleName like '%@Key%'";
            }
            return (await _repository.GetListAsync(conditions, new
            {
                Key = model.Key,
            })).AsList();
        }



        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns></returns>
        public async Task<TableDataModel> LoadDataAsync(ManagerRoleRequestModel model)
        {
            string conditions = "where IsDelete=0 ";//未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += "and RoleName like '%@Key%'";
            }
            return new TableDataModel
            {
                count = await _repository.RecordCountAsync(conditions, new
                {
                    Key = model.Key,
                }),
                data = await _repository.GetListPagedAsync(model.Page, model.Limit, conditions, "Id desc", new
                {
                    Key = model.Key,
                }),
            };
        }

        public async Task<List<MenuNavView>> GetMenusByRoleIdAsync(int roleId)
        {
            var menuList = await _repository.GetMenusByRoleIdAsync(roleId);
            if (menuList?.Count() > 0)
            {
                var menuNavViewList = new List<MenuNavView>();
                menuList.AsParallel().ForAll(x =>
                {
                    var navView = _mapper.Map<MenuNavView>(x);
                    menuNavViewList.Add(navView);
                });
                return menuNavViewList;
            }
            else
            {
                return null;
            }

        }
    }
}