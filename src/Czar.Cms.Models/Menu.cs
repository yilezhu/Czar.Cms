// 本代码由代码生成器生成请勿随意改动
// 生成时间  2018-12-13 10:10:20
using System;

namespace Czar.Cms.Models
{
	/// <summary>
	/// yilezhu
	/// 2018-12-13 10:10:20
	/// 后台管理菜单
	/// </summary>
	public class Menu
	{
				/// <summary>
		/// 主键
		/// </summary>
		public Int32 Id {get;set;}

		/// <summary>
		/// 父菜单ID
		/// </summary>
		public Int32 ParentId {get;set;}

		/// <summary>
		/// 名称
		/// </summary>
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
		public Boolean IsDisplay {get;set;}

		/// <summary>
		/// 是否系统默认
		/// </summary>
		public Boolean IsSystem {get;set;}

		/// <summary>
		/// 添加人
		/// </summary>
		public Int32 AddManagerId {get;set;}

		/// <summary>
		/// 添加时间
		/// </summary>
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
		public Boolean IsDelete {get;set;}


	}
}
