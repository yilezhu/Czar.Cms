/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：ManagerRole列表显示实体                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/16 15:22:10                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.ManagerRole                                   
*│　类    名： ManagerRoleListModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class ManagerRoleListModel
    {
        /// <summary>
		/// 主键
		/// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public String RoleName { get; set; }

        /// <summary>
        /// 角色类型1超管2系管
        /// </summary>
        public Int32 RoleType { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public Boolean IsSystem { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public Int32 AddManagerId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 菜单ID数组  
        /// </summary>
        public virtual int[] MenuIds { get; set; }
    }
}
