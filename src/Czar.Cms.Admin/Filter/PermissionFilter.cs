using Czar.Cms.IServices;
using Czar.Cms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OnActionExecution : ActionFilterAttribute
    {
        private readonly IManagerService _managerService;

        public OnActionExecution(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var identity = context.RouteData.Values["controller"] + "/" + context.RouteData.Values["action"];


            //if (_managerService.Count(
            //        m => m.Identity.Equals(identity, StringComparison.OrdinalIgnoreCase) && m.Activable) <= 0)
            //{

            //        context.Result = new ViewResult() { ViewName = "无权访问" };
            //        context.HttpContext.Response.StatusCode = HttpStatusCode.NotFound.GetHashCode();

            //}

            base.OnActionExecuting(context);
        }



    }
}
