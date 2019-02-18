/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：结果通用key                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/29 17:30:51                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Admin.ResultModel                                 
*│　类    名： ResultCodeAddMsgKeys                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czar.Cms.ViewModels
{
    public class ResultCodeAddMsgKeys
    {
        #region 通用 100
        /// <summary>
        /// 通用成功编码
        /// </summary>
        public const int CommonObjectSuccessCode = 0;
        /// <summary>
        /// 通用操作成功信息
        /// </summary>
        public const string CommonObjectSuccessMsg = "操作成功";
        /// <summary>
        /// 通用Form验证失败错误码
        /// </summary>
        public const int CommonModelStateInvalidCode = 101;
        /// <summary>
        /// 通用Form验证失败错误码
        /// </summary>
        public const string CommonModelStateInvalidMsg = "请求数据校验失败";
        /// <summary>
        /// 数据为空的编码
        /// </summary>
        public const int CommonFailNoDataCode = 102;
        /// <summary>
        /// 数据为空的信息
        /// </summary>
        public const string CommonFailNoDataMsg = "数据不存在";
        /// <summary>
        /// 数据状态发生变化的编码
        /// </summary>
        public const int CommonDataStatusChangeCode = 103;
        /// <summary>
        /// 数据状态发生变化的信息
        /// </summary>
        public const string CommonDataStatusChangeMsg = "数据状态已发生变化，请刷新后再进行操作";

        /// <summary>
        /// 通用失败，系统异常错误码
        /// </summary>
        public const int CommonExceptionCode = 106;
        /// <summary>
        /// 通用失败，系统异常信息
        /// </summary>
        public const string CommonExceptionMsg = "系统异常";
        #endregion

        #region 用户登录
        /// <summary>
        /// 错误次数超过允许最大失败次数
        /// </summary>
        public const int SignInErrorTimesOverTimesCode = 201;
        /// <summary>
        /// 错误次数超过允许最大失败次数
        /// </summary>
        public const string SignInErrorTimesOverTimesMsg = "错误超过3次，请重新打开浏览器后再进行登录";

        /// <summary>
        /// 用户名或者密码错误
        /// </summary>
        public const int SignInPasswordOrUserNameErrorCode = 202;
        /// <summary>
        /// 用户名或者密码错误
        /// </summary>
        public const string SignInPasswordOrUserNameErrorMsg = "对不起，您输入的用户名或者密码错误";

        /// <summary>
        /// 用户被锁定
        /// </summary>
        public const int SignInUserLockedCode = 203;
        /// <summary>
        /// 用户被锁定
        /// </summary>
        public const string SignInUserLockedMsg = "对不起，该账号已锁定，请联系管理员";
        /// <summary>
        /// 验证码错误
        /// </summary>
        public const int SignInCaptchaCodeErrorCode = 204;
        /// <summary>
        /// 验证码错误
        /// </summary>
        public const string SignInCaptchaCodeErrorMsg = "验证码输入有误";

        /// <summary>
        /// 未分配角色
        /// </summary>
        public const int SignInNoRoleErrorCode = 205;
        /// <summary>
        /// 未分配角色
        /// </summary>
        public const string SignInNoRoleErrorMsg = "暂未分配角色，不能进行登录，请联系管理员";

        /// <summary>
        /// 旧密码输入错误
        /// </summary>
        public const int PasswordOldErrorCode = 206;
        /// <summary>
        /// 旧密码输入错误
        /// </summary>
        public const string PasswordOldErrorMsg = "旧密码输入错误";
        #endregion
    }
}
