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
using Microsoft.Extensions.Caching.Memory;

namespace Czar.Cms.Admin.Controllers
{
    public class ManagerController : BaseController
    {
        private readonly IManagerService _service;
        private readonly IManagerRoleService _roleService;
        private readonly IMemoryCache _cache;

        public ManagerController(IManagerService service, IManagerRoleService roleService, IMemoryCache cache)
        {
            _service = service;
            _roleService = roleService;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<string> LoadData([FromQuery]ManagerRequestModel model)
        {
            return JsonHelper.ObjectToJSON(await _service.LoadDataAsync(model));
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
        [Route("/Manager/AddOrModify")]
        public async Task<string> AddOrModifyAsync([FromForm]ManagerAddOrModifyModel item)
        {
            var result = new BaseResult();
            ManagerValidation validationRules = new ManagerValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = await _service.AddOrModifyAsync(item);
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
            return JsonHelper.ObjectToJSON(_service.DeleteIdsAsync(roleId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Manager/ChangeLockStatus")]
        public async Task<string> ChangeLockStatusAsync([FromForm]ChangeStatusModel item)
        {
            var result = new BaseResult();
            ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = await _service.ChangeLockStatusAsync(item);
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
        [Route("/Manager/ChangePassword")]
        public async Task<string> ChangePasswordAsync([FromForm]ChangePasswordModel item)
        {
            var result = new BaseResult();
            if (!ModelState.IsValid)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ToErrorString(ModelState, "||");
            }
            else
            {
                result = await _service.ChangePasswordAsync(item);
            }
            return JsonHelper.ObjectToJSON(result);
        }

        public async Task<IActionResult> ManagerInfo()
        {
            var Id = User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (Id == null)
            {
                return RedirectToAction("SignOut", "Account");
            }
            var model = await _service.GetManagerContainRoleNameByIdAsync(int.Parse(Id.Value));
            if (model == null)
            {
                return RedirectToAction("SignOut", "Account");
            }
            model.Avatar = model.Avatar ?? "/images/userface1.jpg";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Manager/ManagerInfo")]
        public async Task<string> ManagerInfoAsync([FromForm]ChangeInfoModel item)
        {
            
            var result = new BaseResult();
            if (ModelState.IsValid)
            {
                item.ModifyManagerId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);
                item.ModifyTime = DateTime.Now;
                result = await _service.UpdateManagerInfoAsync(item);
                _cache.Set("NickName", item.NickName ?? "匿名", TimeSpan.FromMinutes(15));
                _cache.Set("Email", item.Email ?? "", TimeSpan.FromMinutes(15));
                _cache.Set("Avatar", item.Avatar ?? "/images/userface1.jpg", TimeSpan.FromMinutes(15));
                _cache.Set("Mobile", item.Mobile ?? "", TimeSpan.FromMinutes(15));
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