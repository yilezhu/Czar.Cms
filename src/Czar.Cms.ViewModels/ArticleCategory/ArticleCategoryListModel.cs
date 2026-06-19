/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类列表模型                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
*└──────────────────────────────────────────────────────────────┘
*/
using System;

namespace Czar.Cms.ViewModels
{
    public class ArticleCategoryListModel
    {
        /// <summary>
        /// 主键
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
        /// 显示父分类名称（前端展示用）
        /// </summary>
        public string ParentTitle { get; set; }

        /// <summary>
        /// 类别深度
        /// </summary>
        public Int32? ClassLayer { get; set; }

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
    }
}
