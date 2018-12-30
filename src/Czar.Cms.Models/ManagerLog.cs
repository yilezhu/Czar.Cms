/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：操作日志                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-30 15:21:32                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Models                                  
*│　类    名：ManagerLog                                     
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
	/// 操作日志
	/// </summary>
	[Table("ManagerLog")]
	public class ManagerLog
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 操作类型
		/// </summary>
		[MaxLength(32)]
		public String ActionType {get;set;}

		/// <summary>
		/// 主键
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 AddManageId {get;set;}

		/// <summary>
		/// 操作人名称
		/// </summary>
		[MaxLength(64)]
		public String AddManagerNickName {get;set;}

		/// <summary>
		/// 操作时间
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}

		/// <summary>
		/// 操作IP
		/// </summary>
		[MaxLength(64)]
		public String AddIp {get;set;}

		/// <summary>
		/// 备注
		/// </summary>
		[MaxLength(256)]
		public String Remark {get;set;}


	}
}
