/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类新增或修改模型                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
*└──────────────────────────────────────────────────────────────┘
*/
using System;

namespace Czar.Cms.ViewModels
{
    public class ArticleCategoryAddOrModifyModel
    {
        /// <summary>
        /// 主键（新增时为0）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分类标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 父分类ID
        /// </summary>
        public Int32 ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string SeoTitle { get; set; }

        /// <summary>
        /// SEO关键字
        /// </summary>
        public string SeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string SeoDescription { get; set; }
    }
}
