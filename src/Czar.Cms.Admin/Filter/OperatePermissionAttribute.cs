/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：操作级权限校验属性
*│　用于标记某个 Action 需要额外的操作权限校验（如删除、修改等）
*└──────────────────────────────────────────────────────────────┘
*/
using System;

namespace Czar.Cms.Admin.Filter
{
    /// <summary>
    /// 标记某个 Action 需要操作级权限校验。
    /// 配合 PermissionFilter 使用，在基础路径校验之上增加操作粒度的权限控制。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OperatePermissionAttribute : Attribute
    {
        /// <summary>
        /// 操作名称，如 "Delete"、"ChangeStatus" 等
        /// </summary>
        public string OperateName { get; }

        public OperatePermissionAttribute(string operateName = null)
        {
            OperateName = operateName;
        }
    }
}
