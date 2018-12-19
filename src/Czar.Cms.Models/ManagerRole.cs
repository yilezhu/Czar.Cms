/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员角色                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-18 13:28:43                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：ManagerRole                                     
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
	/// 后台管理员角色
	/// </summary>
	[Table("ManagerRole")]
	public class ManagerRole
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 角色名称
		/// </summary>
		[Required]
		public String RoleName {get;set;}

		/// <summary>
		/// 角色类型1超管2系管
		/// </summary>
		[Required]
		public Int32 RoleType {get;set;}

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
		public DateTime? ModifyTime {get;set;}

		/// <summary>
		/// 是否删除
		/// </summary>
		[Required]
		public Boolean IsDelete {get;set;}

		/// <summary>
		/// 备注
		/// </summary>
		public String Remark {get;set;}


	}
}
