/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-18 13:28:43                            
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
	/// 2018-12-18 13:28:43
	/// 文章
	/// </summary>
	[Table("Article")]
	public class Article
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
		public Int32 CategoryId {get;set;}

		/// <summary>
		/// 文章标题
		/// </summary>
		[Required]
		public String Title {get;set;}

		/// <summary>
		/// 图片地址
		/// </summary>
		public String ImageUrl {get;set;}

		/// <summary>
		/// 文章内容
		/// </summary>
		public String Content {get;set;}

		/// <summary>
		/// 浏览次数
		/// </summary>
		[Required]
		public Int32 ViewCount {get;set;}

		/// <summary>
		/// 排序
		/// </summary>
		[Required]
		public Int32 Sort {get;set;}

		/// <summary>
		/// 作者
		/// </summary>
		public String Author {get;set;}

		/// <summary>
		/// 来源
		/// </summary>
		public String Source {get;set;}

		/// <summary>
		/// SEO标题
		/// </summary>
		public String SeoTitle {get;set;}

		/// <summary>
		/// SEO关键字
		/// </summary>
		public String SeoKeyword {get;set;}

		/// <summary>
		/// SEO描述
		/// </summary>
		public String SeoDescription {get;set;}

		/// <summary>
		/// 添加人ID
		/// </summary>
		[Required]
		public Int32 AddManagerId {get;set;}

		/// <summary>
		/// 添加时间
		/// </summary>
		[Required]
		public DateTime AddTime {get;set;}

		/// <summary>
		/// 修改人ID
		/// </summary>
		public Int32? ModifyManagerId {get;set;}

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? ModifyTime {get;set;}

		/// <summary>
		/// 是否置顶
		/// </summary>
		[Required]
		public Boolean IsTop {get;set;}

		/// <summary>
		/// 是否轮播显示
		/// </summary>
		[Required]
		public Boolean IsSlide {get;set;}

		/// <summary>
		/// 是否热门
		/// </summary>
		[Required]
		public Boolean IsRed {get;set;}

		/// <summary>
		/// 是否发布
		/// </summary>
		[Required]
		public Boolean IsPublish {get;set;}

		/// <summary>
		/// 是否删除
		/// </summary>
		[Required]
		public Boolean IsDeleted {get;set;}


	}
}
