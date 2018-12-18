/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-18 13:28:43                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：Manager                                     
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
	/// 后台管理员
	/// </summary>
	[Table("Manager")]
	public class Manager
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 角色ID
		/// </summary>
		[Required]
		public Int32 RoleId {get;set;}

		/// <summary>
		/// 用户名
		/// </summary>
		[Required]
		public String UserName {get;set;}

		/// <summary>
		/// 密码
		/// </summary>
		[Required]
		public String Password {get;set;}

		/// <summary>
		/// 头像
		/// </summary>
		public String Avatar {get;set;}

		/// <summary>
		/// 用户昵称
		/// </summary>
		public String NickName {get;set;}

		/// <summary>
		/// 手机号码
		/// </summary>
		public String Mobile {get;set;}

		/// <summary>
		/// 邮箱地址
		/// </summary>
		public String Email {get;set;}

		/// <summary>
		/// 登录次数
		/// </summary>
		public Int32? LoginCount {get;set;}

		/// <summary>
		/// 最后一次登录IP
		/// </summary>
		public String LoginLastIp {get;set;}

		/// <summary>
		/// 最后一次登录时间
		/// </summary>
		public DateTime? LoginLastTime {get;set;}

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
		/// 是否锁定
		/// </summary>
		[Required]
		public Boolean IsLock {get;set;}

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
