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

namespace Czar.Cms.Admin.ResultModel
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
        public const string CommonFailNoDataMsg = "传输过来的数据为空";
        
        /// <summary>
        /// 通用失败，系统异常错误码
        /// </summary>
        public const int CommonExceptionCode = 106;
        /// <summary>
        /// 通用失败，系统异常信息
        /// </summary>
        public const string CommonExceptionMsg = "系统异常";
        #endregion
    }
}
