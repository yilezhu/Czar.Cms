/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：HttpContext扩展类                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/22 22:34:52                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Core.Extensions                                   
*│　类    名： HttpContextExtensions                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Czar.Cms.Core.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取客户Ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 判断是否ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.ContainsKey("X-Requested-With") &&
                   request.Headers["X-Requested-With"].Equals("XMLHttpRequest");
        }
    }
}
