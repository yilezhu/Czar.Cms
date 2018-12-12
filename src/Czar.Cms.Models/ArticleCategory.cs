// 本代码由代码生成器生成请勿随意改动
// 生成时间  2018-12-12 23:06:30
using System;

namespace Czar.Cms.Models
{
	/// <summary>
	/// yilezhu
	/// 2018-12-12 23:06:30
	/// 文章分类
	/// </summary>
	public class ArticleCategory
	{
				/// <summary>
		/// 主键
		/// </summary>
		public Int32 Id {get;set;}

		/// <summary>
		/// 分类标题
		/// </summary>
		public String Title {get;set;}

		/// <summary>
		/// 父分类ID
		/// </summary>
		public Int32 ParentId {get;set;}

		/// <summary>
		/// 类别ID列表(逗号分隔开)
		/// </summary>
		public String ClassList {get;set;}

		/// <summary>
		/// 类别深度
		/// </summary>
		public Int32? ClassLayer {get;set;}

		/// <summary>
		/// 排序
		/// </summary>
		public Int32 Sort {get;set;}

		/// <summary>
		/// 分类图标
		/// </summary>
		public String ImageUrl {get;set;}

		/// <summary>
		/// 分类SEO标题
		/// </summary>
		public String SeoTitle {get;set;}

		/// <summary>
		/// 分类SEO关键字
		/// </summary>
		public String SeoKeywords {get;set;}

		/// <summary>
		/// 分类SEO描述
		/// </summary>
		public String SeoDescription {get;set;}

		/// <summary>
		/// 是否删除
		/// </summary>
		public Boolean IsDeleted {get;set;}


	}
}
