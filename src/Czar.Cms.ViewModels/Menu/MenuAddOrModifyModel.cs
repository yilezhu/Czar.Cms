/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：Menu更新或者新增实体                                                     
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/31 20:11:46                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.Manager                                   
*│　类    名： ManagerAddOrModifyModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class MenuAddOrModifyModel
    {
        /// <summary>
		/// 主键
		/// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public Int32 ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public String DisplayName { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public String IconUrl { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public String LinkUrl { get; set; }

        /// <summary>
        /// 排序数字
        /// </summary>
        public Int32? Sort { get; set; }

        /// <summary>
        /// 操作权限（按钮权限时使用）
        /// </summary>
        public String Permission { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean IsDisplay { get; set; }

        /// <summary>
        /// 是否系统默认,系统默认不能删除
        /// </summary>
        public Boolean IsSystem { get; set; }
    }
}
