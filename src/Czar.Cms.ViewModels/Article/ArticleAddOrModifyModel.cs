/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章新增或修改模型                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/5/12 10:00:00                             
*└──────────────────────────────────────────────────────────────┘
*/
namespace Czar.Cms.ViewModels
{
    public class ArticleAddOrModifyModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string SeoTitle { get; set; }

        /// <summary>
        /// SEO关键字
        /// </summary>
        public string SeoKeyword { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string SeoDescription { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否轮播
        /// </summary>
        public bool IsSlide { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsRed { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
    }
}
