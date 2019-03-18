////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：定时任务                                                    
*│　作    者：yilezhu                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2019-03-13 11:17:00                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Services                                  
*│　类    名： TaskInfoService                                    
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
    public class TaskInfoService : ITaskInfoService
    {
        private readonly ITaskInfoRepository _repository;
        private readonly IMapper _mapper;

        public TaskInfoService(ITaskInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TableDataModel> LoadDataAsync(TaskInfoRequestModel model)
        {
            string conditions = "";
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"where Name like '%@Key%'";
            }

            return new TableDataModel
            {
                count = await _repository.RecordCountAsync(conditions),
                data = await _repository.GetListPagedAsync(model.Page, model.Limit, conditions, "Id desc", new
                {
                    Key = model.Key,
                }),
            };
        }

        public Task<bool> ResumeSystemStoppedAsync()
        {
            return _repository.ResumeSystemStoppedAsync();
        }

        public async Task<bool> SystemStoppedAsync()
        {
            return await _repository.SystemStoppedAsync();

        }

        public async Task<BooleanResult> UpdateStatusByIdsAsync(int[] ids, int Status)
        {
            return new BooleanResult
            {
                Data = await _repository.UpdateStatusByIdsAsync(ids, Status)
            };
        }


        public async Task<List<TaskInfoDto>> GetListByJobStatuAsync(int Status)
        {
            var result = await _repository.GetListByJobStatuAsync(Status);
          
            return _mapper.Map<List<TaskInfoDto>>(result);
        }

        /// <summary>
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<BooleanResult> IsExistsNameAsync(TaskInfoAddOrModifyModel item)
        {
            bool data = false;
            if (item.Id > 0)
            {
                data = await _repository.IsExistsNameAsync(item.Name, item.Id);
            }
            else
            {
                data = await _repository.IsExistsNameAsync(item.Name);

            }
            return new BooleanResult
            {
                Data = data,
            };
        }

        public async Task<BaseResult> AddOrModifyAsync(TaskInfoAddOrModifyModel item)
        {
            var result = new BaseResult();
            TaskInfo model;
            if (item.Id == 0)
            {
                //TODO ADD
                model = _mapper.Map<TaskInfo>(item);
                model.Status = (int)TaskInfoStatus.Stopped;
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

        public async Task<BooleanResult> DeleteAsync(int Id)
        {
            return new BooleanResult {
                Data=await _repository.DeleteAsync(Id)>0,
            };
        }

        public async Task<List<TaskInfoDto>> GetListByIdsAsync(int[] ids)
        {
            string conditions = "where Id in @ids";
            var lst = await _repository.GetListAsync(conditions, new { ids = ids });
            return _mapper.Map<List<TaskInfoDto>>(lst);
        }

        public async Task<TaskInfoDto> GetByIdAsync(int id)
        {

            var item = await _repository.GetAsync(id);
            return _mapper.Map<TaskInfoDto>(item);
        }
    }
}