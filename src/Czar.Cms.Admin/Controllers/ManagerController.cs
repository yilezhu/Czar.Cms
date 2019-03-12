using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Czar.Cms.Models;
using Czar.Cms.IRepository;
using System.Text;
using Czar.Cms.Admin.Validation;
using FluentValidation.Results;
using Czar.Cms.ViewModels;
using Czar.Cms.Core.Extensions;
using Czar.Cms.Core.Helper;
using AutoMapper;
using Czar.Cms.IServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Czar.Cms.Admin.Controllers
{
    public class ManagerController : BaseController
    {
        private readonly IManagerService _service;
        private readonly IManagerRoleService _roleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManagerController(IManagerService service, IManagerRoleService roleService, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }


        public string LoadData([FromQuery]ManagerRequestModel model)
        {
            return JsonHelper.ObjectToJSON(_service.LoadData(model));
        }

        [HttpGet]
        public IActionResult AddOrModify()
        {
            var roleList = _roleService.GetListByCondition(new ManagerRoleRequestModel
            {
                Key = null
            });
            return View(roleList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddOrModify([FromForm]ManagerAddOrModifyModel item)
        {
            var result = new BaseResult();
            ManagerValidation validationRules = new ManagerValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = _service.AddOrModify(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete(int[] roleId)
        {
            return JsonHelper.ObjectToJSON(_service.DeleteIds(roleId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ChangeLockStatus([FromForm]ChangeStatusModel item)
        {
            var result = new BaseResult();
            ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = _service.ChangeLockStatus(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        public IActionResult ChangePassword()
        {
            ViewData["NickName"] = User.Claims.FirstOrDefault(x => x.Type == "NickName")?.Value;
            ViewData["Id"] = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ChangePassword([FromForm]ChangePasswordModel item)
        {
            var result = new BaseResult();
            if (!ModelState.IsValid)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ToErrorString(ModelState, "||");
            }
            else
            {
                result = _service.ChangePassword(item);
            }
            return JsonHelper.ObjectToJSON(result);
        }

        public IActionResult ManagerInfo()
        {
            var Id = User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (Id == null)
            {
                return RedirectToAction("SignOut", "Account");
            }
            var model = _service.GetManagerContainRoleNameById(int.Parse(Id.Value));
            if (model == null)
            {
                return RedirectToAction("SignOut", "Account");
            }
            model.Avatar = model.Avatar ?? "/images/userface1.jpg";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ManagerInfo([FromForm]ChangeInfoModel item)
        {
            
            var result = new BaseResult();
            if (ModelState.IsValid)
            {
                item.ModifyManagerId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);
                item.ModifyTime = DateTime.Now;
                result = _service.UpdateManagerInfo(item);
                _httpContextAccessor.HttpContext.Session.SetString("NickName", item.NickName ?? "匿名");
                _httpContextAccessor.HttpContext.Session.SetString("Email", item.Email ?? "");
                _httpContextAccessor.HttpContext.Session.SetString("Avatar", item.Avatar ?? "/images/userface1.jpg");
                _httpContextAccessor.HttpContext.Session.SetString("Mobile", item.Mobile ?? "");
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ToErrorString(ModelState, "||");
            }
            
            return JsonHelper.ObjectToJSON(result);
        }

    }
}