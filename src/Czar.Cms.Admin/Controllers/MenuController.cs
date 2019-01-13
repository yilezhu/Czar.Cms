using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.Admin.Validation;
using Czar.Cms.Core.Helper;
using Czar.Cms.IServices;
using Czar.Cms.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Czar.Cms.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string LoadData([FromQuery]MenuRequestModel model)
        {
            return JsonHelper.ObjectToJSON(_service.LoadData(model));
        }

        [HttpGet]
        public IActionResult AddOrModify()
        {
            return View(_service.GetChildListByParentId(0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddOrModify([FromForm]MenuAddOrModifyModel item)
        {
            var result = new BaseResult();
            MenuValidation validationRules = new MenuValidation();
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
        public string Delete(int[] menuId)
        {
            return JsonHelper.ObjectToJSON(_service.DeleteIds(menuId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ChangeDisplayStatus([FromForm]ChangeStatusModel item)
        {
            var result = new BaseResult();
            ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = _service.ChangeDisplayStatus(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpGet]
        public string IsExistsName([FromQuery]MenuAddOrModifyModel item)
        {
            var result = _service.IsExistsName(item);
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpGet]
        public string LoadDataWithParentId([FromQuery]int ParentId=-1)
        {
            return JsonHelper.ObjectToJSON(_service.GetChildListByParentId(ParentId));
        }
    }
}