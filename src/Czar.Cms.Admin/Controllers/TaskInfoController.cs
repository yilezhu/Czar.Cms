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
    public class TaskInfoController : BaseController
    {
        private readonly ITaskInfoService _service;

        public TaskInfoController(ITaskInfoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<string> LoadData([FromQuery]TaskInfoRequestModel model)
        {
            return JsonHelper.ObjectToJSON(await _service.LoadDataAsync(model));
        }

        [HttpGet]
        public IActionResult AddOrModify()
        {
            return View();

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
                //result = _service.AddOrModify(item);
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
            //return JsonHelper.ObjectToJSON(_service.DeleteIds(menuId));
            return JsonHelper.ObjectToJSON("");

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
               // result = _service.ChangeDisplayStatus(item);
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
            //var result = _service.IsExistsName(item);
            return JsonHelper.ObjectToJSON("");
        }
    }
}