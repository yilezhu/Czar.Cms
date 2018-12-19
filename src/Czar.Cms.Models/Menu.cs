/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理菜单                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-18 13:28:43                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：Menu                                     
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
	/// 后台管理菜单
	/// </summary>
	[Table("Menu")]
	public class Menu
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 父菜单ID
		/// </summary>
		[Required]
		public Int32 ParentId {get;set;}

		/// <summary>
		/// 名称
		/// </summary>
		[Required]
		public String Name {get;set;}

		/// <summary>
		/// 显示名称
		/// </summary>
		public String DisplayName {get;set;}

		/// <summary>
		/// 图标地址
		/// </summary>
		public String IconUrl {get;set;}

		/// <summary>
		/// 链接地址
		/// </summary>
		public String LinkUrl {get;set;}

		/// <summary>
		/// 排序数字
		/// </summary>
		public Int32? Sort {get;set;}

		/// <summary>
		/// 操作权限（按钮权限时使用）
		/// </summary>
		public String Permission {get;set;}

		/// <summary>
		/// 是否显示
		/// </summary>
		[Required]
		public Boolean IsDisplay {get;set;}

		/// <summary>
		/// 是否系统默认
		/// </summary>
		[Required]
		public Boolean IsSystem {get;set;}

		/// <summary>
		/// 添加人
		/// </summary>
		[Required]
		public Int32 AddManagerId {get;set;}

		/// <summary>
		/// 添加时间
		/// </summary>
		[Required]
		public DateTime AddTime {get;set;}

		/// <summary>
		/// 修改人
		/// </summary>
		public Int32? ModifyManagerId {get;set;}

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? ModifyTimes {get;set;}

		/// <summary>
		/// 是否删除
		/// </summary>
		[Required]
		public Boolean IsDelete {get;set;}


	}
}
