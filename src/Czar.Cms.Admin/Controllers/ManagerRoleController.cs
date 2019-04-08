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

namespace Czar.Cms.Admin.Controllers
{
    public class ManagerRoleController : BaseController
    {
        private readonly IManagerRoleService _service ;
        private readonly IRolePermissionService _rolePermissionService;

        public ManagerRoleController(IManagerRoleService service, IRolePermissionService rolePermissionService)
        {
            _service = service;
            _rolePermissionService = rolePermissionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public string LoadData([FromQuery]ManagerRoleRequestModel model)
        {
            return JsonHelper.ObjectToJSON(_service.LoadDataAsync(model));
        }

        [HttpGet]
        public IActionResult AddOrModify(int id)
        {
            if (id > 0)
            {
               ViewData["MenuIds"] = _rolePermissionService.GetIdsByRoleId(id).ArrayToString();
            }
            return View();
        }

        [HttpPost, ActionName("AddOrModify")]
        [ValidateAntiForgeryToken]
        public async Task<string> AddOrModifyAsync([FromForm]ManagerRoleAddOrModifyModel item)
        {
            var result = new BaseResult();
            ManagerRoleValidation validationRules = new ManagerRoleValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result =await _service.AddOrModifyAsync(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            return JsonHelper.ObjectToJSON(result) ;
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<string> DeleteAsync(int[] roleId)
        {
            return JsonHelper.ObjectToJSON(await _service.DeleteIdsAsync(roleId)) ;
        }
    }
}