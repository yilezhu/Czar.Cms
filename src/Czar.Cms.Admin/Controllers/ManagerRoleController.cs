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

        public ManagerRoleController(IManagerRoleService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public string LoadData([FromQuery]ManagerRoleRequestModel model)
        {
            return JsonHelper.ObjectToJSON(_service.LoadData(model));
        }

        [HttpGet]
        public IActionResult AddOrModify()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddOrModify([FromForm]ManagerRoleAddOrModifyModel item)
        {
            var result = new BaseResult();
            ManagerRoleValidation validationRules = new ManagerRoleValidation();
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
            return JsonHelper.ObjectToJSON(result) ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete(int[] roleId)
        {
            return JsonHelper.ObjectToJSON(_service.DeleteIds(roleId)) ;
        }
    }
}