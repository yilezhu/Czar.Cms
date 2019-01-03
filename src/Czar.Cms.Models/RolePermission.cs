/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：角色权限表                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-30 15:21:32                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：RolePermission                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Czar.Cms.Models
{
	/// <summary>
	/// yilezhu
	/// 2018-12-30 15:21:32
	/// 角色权限表
	/// </summary>
	[Table("RolePermission")]
	public class RolePermission
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 角色主键
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 RoleId {get;set;}

		/// <summary>
		/// 菜单主键
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 MenuId {get;set;}

		/// <summary>
		/// 操作类型（功能权限）
		/// </summary>
		[MaxLength(128)]
		public String Permission {get;set;}


	}
}
