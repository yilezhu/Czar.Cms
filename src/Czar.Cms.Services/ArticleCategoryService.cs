/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类服务实现                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
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
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IArticleCategoryRepository _repository;
        private readonly IMapper _mapper;

        public ArticleCategoryService(IArticleCategoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        public async Task<TableDataModel> LoadDataAsync(ArticleCategoryRequestModel model)
        {
            string conditions = "where IsDeleted=0 ";
            object param = null;
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += "and Title like @Key";
                param = new { Key = $"%{model.Key}%" };
            }

            var list = (await _repository.GetListPagedAsync(model.Page, model.Limit, conditions, "Id desc", param)).ToList();
            var viewList = _mapper.Map<List<ArticleCategoryListModel>>(list);

            // 填充父分类名称
            if (viewList?.Count > 0)
            {
                var parentIds = viewList.Select(x => x.ParentId).Distinct().Where(p => p > 0).ToArray();
                if (parentIds.Length > 0)
                {
                    var parentList = (await _repository.GetListAsync("where Id in @Ids", new { Ids = parentIds })).ToList();
                    foreach (var item in viewList)
                    {
                        var parent = parentList.FirstOrDefault(p => p.Id == item.ParentId);
                        if (parent != null) item.ParentTitle = parent.Title;
                    }
                }
            }

            return new TableDataModel
            {
                count = await _repository.RecordCountAsync(conditions, param),
                data = viewList,
            };
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        public async Task<BaseResult> AddOrModifyAsync(ArticleCategoryAddOrModifyModel item)
        {
            var result = new BaseResult();
            ArticleCategory model;
            if (item.Id == 0)
            {
                model = _mapper.Map<ArticleCategory>(item);
                model.IsDeleted = false;
                model.ClassList = "";
                model.ClassLayer = 1;

                // 计算层级与路径：如果有父级则继承父层的 ClassList 与 ClassLayer+1
                if (item.ParentId > 0)
                {
                    var parent = await _repository.GetAsync(item.ParentId);
                    if (parent != null)
                    {
                        model.ClassLayer = parent.ClassLayer + 1;
                        model.ClassList = string.IsNullOrEmpty(parent.ClassList) ? parent.Id.ToString() : parent.ClassList + "," + parent.Id;
                    }
                }

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

        /// <summary>
        /// 获取所有分类
        /// </summary>
        public async Task<List<ArticleCategory>> GetAllListAsync()
        {
            return (await _repository.GetListAsync("where IsDeleted=0", new { })).ToList();
        }
    }
}
