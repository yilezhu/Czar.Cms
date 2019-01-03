/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：全局异常过滤                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0                                              
*│　创建时间：2018-12-29 17:28:43                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Czar.Cms.Admin.Filter                                 
*│　类    名：GlobalExceptionFilter                                     
*└──────────────────────────────────────────────────────────────┘
*/

using Czar.Cms.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Filter
{
    public class GlobalExceptionFilter: IExceptionFilter
    {
       public static Logger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {
            logger.Error(filterContext.Exception);
            var result = new BaseResult()
            {
                ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode,//系统异常代码
                ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg,//系统异常信息
            };
            filterContext.Result = new ObjectResult(result);
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.ExceptionHandled = true;
        }
    }
}
