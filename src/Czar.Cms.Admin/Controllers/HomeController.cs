using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Czar.Cms.Admin.Models;
using System.Security.Claims;
using Czar.Cms.Core.Extensions;
using Czar.Cms.ViewModels;
using Czar.Cms.IServices;
using Czar.Cms.Core.Helper;
using Microsoft.AspNetCore.Http;

namespace Czar.Cms.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IManagerRoleService _managerRoleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IManagerRoleService managerRoleService, IHttpContextAccessor httpContextAccessor)
        {
            _managerRoleService = managerRoleService;
            _httpContextAccessor = httpContextAccessor;
        }



        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["NickName"] = _httpContextAccessor.HttpContext.Session.GetString("NickName");
            ViewData["Avatar"] = _httpContextAccessor.HttpContext.Session.GetString("Avatar");

            return View();
        }

        /// <summary>
        /// 控制中心
        /// </summary>
        /// <returns></returns>
        public IActionResult Main()
        {
            ViewData["LoginCount"] = User.Claims.FirstOrDefault(x => x.Type == "LoginCount")?.Value;
            ViewData["LoginLastIp"] = User.Claims.FirstOrDefault(x => x.Type == "LoginLastIp")?.Value;
            ViewData["LoginLastTime"] = User.Claims.FirstOrDefault(x => x.Type == "LoginLastTime")?.Value;
            return View();
        }


        public string GetMenu()
        {
            var roleId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var navViewTree = _managerRoleService.GetMenusByRoleId(Int32.Parse(roleId)).GenerateTree(x => x.Id, x => x.ParentId);
            return JsonHelper.ObjectToJSON(navViewTree);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
