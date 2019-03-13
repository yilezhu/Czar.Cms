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
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Services
{
    public class TaskInfoService: ITaskInfoService
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
            string conditions = "where IsDelete=0 ";//未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"and Name like '%@Key%'";
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

        public async Task<bool> UpdateStatusByIdsAsync(int[] ids, int Status)
        {
            return await _repository.UpdateStatusByIdsAsync(ids,Status);
        }


        public async Task<List<TaskInfoDto>> GetListByJobStatuAsync(int Status)
        {
            var result= await _repository.GetListByJobStatuAsync(Status);
            return _mapper.Map<List<TaskInfoDto>>(result);
        }
    }
}