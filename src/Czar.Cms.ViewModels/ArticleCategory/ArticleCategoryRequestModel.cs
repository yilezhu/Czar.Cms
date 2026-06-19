/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类分页请求模型                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
*└──────────────────────────────────────────────────────────────┘
*/
using System;

namespace Czar.Cms.ViewModels
{
    public class ArticleCategoryRequestModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 每页数量
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// 关键字搜索
        /// </summary>
        public string Key { get; set; }
    }
}
