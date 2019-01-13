/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：ManagerRole更新或者新增实体                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/30 20:58:12                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.ManagerRole                                   
*│　类    名： ManagerRoleAddOrModifyModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class ManagerRoleAddOrModifyModel
    {
        /// <summary>
		/// 主键
		/// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 角色名称
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
        /// 备注
        /// </summary>
        public String Remark { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public int[] MenuIds { get; set; }
    }
}
