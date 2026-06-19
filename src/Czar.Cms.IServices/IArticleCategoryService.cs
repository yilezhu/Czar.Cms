/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类服务接口                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czar.Cms.IServices
{
    public interface IArticleCategoryService
    {
        /// <summary>
        /// 根据查询条件获取分页列表
        /// </summary>
        Task<TableDataModel> LoadDataAsync(ArticleCategoryRequestModel model);

        /// <summary>
        /// 新增或者修改
        /// </summary>
        Task<BaseResult> AddOrModifyAsync(ArticleCategoryAddOrModifyModel model);

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        Task<BaseResult> DeleteIdsAsync(int[] Ids);

        /// <summary>
        /// 获取所有分类（用于下拉选择）
        /// </summary>
        Task<List<ArticleCategory>> GetAllListAsync();
    }
}
