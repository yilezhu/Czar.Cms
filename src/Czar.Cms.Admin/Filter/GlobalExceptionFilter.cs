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
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Czar.Cms.Admin.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            _logger.LogError(filterContext.Exception, "全局异常捕获");
            var result = new BaseResult()
            {
                ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode,
                ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg,
            };
            filterContext.Result = new ObjectResult(result);
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.ExceptionHandled = true;
        }
    }
}
