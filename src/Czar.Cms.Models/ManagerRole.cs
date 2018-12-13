// 本代码由代码生成器生成请勿随意改动
// 生成时间  2018-12-13 10:10:20
using System;

namespace Czar.Cms.Models
{
	/// <summary>
	/// yilezhu
	/// 2018-12-13 10:10:20
	/// 后台管理员角色
	/// </summary>
	public class ManagerRole
	{
				/// <summary>
		/// 主键
		/// </summary>
		public Int32 Id {get;set;}

		/// <summary>
		/// 角色名称
		/// </summary>
		public String RoleName {get;set;}

		/// <summary>
		/// 角色类型1超管2系管
		/// </summary>
		public Int32 RoleType {get;set;}

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
		public DateTime? ModifyTime {get;set;}

		/// <summary>
		/// 是否删除
		/// </summary>
		public Boolean IsDelete {get;set;}

		/// <summary>
		/// 备注
		/// </summary>
		public String Remark {get;set;}


	}
}
