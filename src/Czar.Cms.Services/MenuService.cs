
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理菜单                                                    
*│　作    者：yilezhu                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2018-12-31 16:43:28                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Services                                  
*│　类    名： MenuService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using AutoMapper;
using Czar.Cms.Core.Extensions;
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
    public class MenuService: IMenuService
    {
        private readonly IMenuRepository _repository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResult> AddOrModifyAsync(MenuAddOrModifyModel item)
        {
            var result = new BaseResult();
            Menu model;
            if (item.Id == 0)
            {
                //TODO ADD
                model = _mapper.Map<Menu>(item);
                model.AddManagerId = 1;
                model.IsDelete = false;
                model.AddTime = DateTime.Now;
                if (await _repository.InsertAsync(model) > 0)
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
                model = await _repository.GetAsync(item.Id);
                if (model != null)
                {
                    _mapper.Map(item, model);
                    model.ModifyManagerId = 1;
                    model.ModifyTime = DateTime.Now;
                    if (await _repository.UpdateAsync(model) > 0)
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

        public TableDataModel LoadData(MenuRequestModel model)
        {
            string conditions = "where IsDelete=0 ";//未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"and DisplayName like '%@Key%'";
            }
           
            return new TableDataModel
            {
                count = _repository.RecordCount(conditions),
                data = _repository.GetListPaged(model.Page, model.Limit, conditions, "Id desc",new {
                    Key=model.Key,
                }).ToList(),
            };
        }

        public async Task<BaseResult> ChangeDisplayStatusAsync(ChangeStatusModel model)
        {
            var result = new BaseResult();
            //判断状态是否发生变化，没有则修改，有则返回状态已变化无法更改状态的提示
            var isLock = await _repository.GetDisplayStatusByIdAsync(model.Id);
            if (isLock == !model.Status)
            {
                var count = await _repository.ChangeDisplayStatusByIdAsync(model.Id, model.Status);
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
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<BooleanResult> IsExistsNameAsync(MenuAddOrModifyModel item)
        {
            bool data = false;
            if (item.Id > 0)
            {
                data =await _repository.IsExistsNameAsync(item.Name, item.Id);
            }
            else
            {
                data =await _repository.IsExistsNameAsync(item.Name);

            }
            var result = new BooleanResult {
                Data= data,
            };
            return result;
        }

        public List<Menu> GetChildListByParentId(int ParentId)
        {
            string conditions = "where IsDelete=0 ";//未删除的
            if (ParentId >= 0)
            {
                conditions += " and ParentId =@ParentId";
            }
            return _repository.GetList(conditions,new {
                ParentId= ParentId
            }).ToList();
           
        }
    }
}