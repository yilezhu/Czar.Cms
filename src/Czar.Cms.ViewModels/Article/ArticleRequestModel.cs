/*
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：ArticleRequestModel                                                   
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/5/11 22:10:17                          
*└──────────────────────────────────────────────────────────────┘
*/
namespace Czar.Cms.ViewModels
{
    public class ArticleRequestModel : PageModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }
    }
}
