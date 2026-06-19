/*
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章服务实现                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019-03-07 16:50:56                             
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
using System.Threading.Tasks;

namespace Czar.Cms.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        public async Task<TableDataModel> LoadDataAsync(ArticleRequestModel model)
        {
            string conditions = "where IsDeleted=0 ";
            object param = null;
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += "and Title like @Key";
                param = new { Key = $"%{model.Key}%" };
            }
            var list = (await _repository.GetListPagedAsync(model.Page, model.Limit, conditions, "Id desc", param)).ToList();
            var viewList = _mapper.Map<List<ArticleListModel>>(list);

            return new TableDataModel
            {
                count = await _repository.RecordCountAsync(conditions, param),
                data = viewList,
            };
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        public async Task<BaseResult> AddOrModifyAsync(ArticleAddOrModifyModel item)
        {
            var result = new BaseResult();
            Article model;
            if (item.Id == 0)
            {
                model = _mapper.Map<Article>(item);
                model.AddTime = DateTime.Now;
                model.AddManagerId = 1;
                model.ViewCount = 0;
                model.IsDeleted = false;

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

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        public async Task<BaseResult> DeleteIdsAsync(int[] Ids)
        {
            var result = new BaseResult();
            if (Ids == null || Ids.Length == 0)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ResultCodeAddMsgKeys.CommonModelStateInvalidMsg;
            }
            else
            {
                var count = await _repository.DeleteLogicalAsync(Ids);
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
            return result;
        }
    }
}
