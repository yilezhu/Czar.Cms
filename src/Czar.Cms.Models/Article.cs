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
*│　描    述：文章                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-03-07 16:50:56                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：Article                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Czar.Cms.Models
{
	/// <summary>
	/// yilezhu
	/// 2019-03-07 16:50:56
	/// 文章
	/// </summary>
	public partial class Article
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 分类ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 CategoryId {get;set;}

		/// <summary>
		/// 文章标题
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String Title {get;set;}

		/// <summary>
		/// 图片地址
		/// </summary>
		[MaxLength(128)]
		public String ImageUrl {get;set;}

		/// <summary>
		/// 文章内容
		/// </summary>
		[MaxLength(2147483647)]
		public String Content {get;set;}

		/// <summary>
		/// 浏览次数
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 ViewCount {get;set;}

		/// <summary>
		/// 排序
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Sort {get;set;}

		/// <summary>
		/// 作者
		/// </summary>
		[MaxLength(64)]
		public String Author {get;set;}

		/// <summary>
		/// 来源
		/// </summary>
		[MaxLength(128)]
		public String Source {get;set;}

		/// <summary>
		/// SEO标题
		/// </summary>
		[MaxLength(128)]
		public String SeoTitle {get;set;}

		/// <summary>
		/// SEO关键字
		/// </summary>
		[MaxLength(256)]
		public String SeoKeyword {get;set;}

		/// <summary>
		/// SEO描述
		/// </summary>
		[MaxLength(512)]
		public String SeoDescription {get;set;}

		/// <summary>
		/// 添加人ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 AddManagerId {get;set;}

		/// <summary>
		/// 添加时间
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}

		/// <summary>
		/// 修改人ID
		/// </summary>
		[MaxLength(10)]
		public Int32? ModifyManagerId {get;set;}

		/// <summary>
		/// 修改时间
		/// </summary>
		[MaxLength(23)]
		public DateTime? ModifyTime {get;set;}

		/// <summary>
		/// 是否置顶
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsTop {get;set;}

		/// <summary>
		/// 是否轮播显示
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsSlide {get;set;}

		/// <summary>
		/// 是否热门
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsRed {get;set;}

		/// <summary>
		/// 是否发布
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsPublish {get;set;}

		/// <summary>
		/// 是否删除
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsDeleted {get;set;}


	}
}
