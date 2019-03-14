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
        public async Task<string> AddOrModify([FromForm]TaskInfoAddOrModifyModel item)
        {
            var result = new BaseResult();
            TaskInfoValidation validationRules = new TaskInfoValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                item.AddManagerId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value);
                item.AddTime = DateTime.Now;
                result = await _service.AddOrModifyAsync(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ToErrorString(ModelState, "||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Stop")]
        public async Task<string> StopAsync(int[] Ids)
        {
            return JsonHelper.ObjectToJSON(await _service.UpdateStatusByIdsAsync(Ids,(int)TaskInfoStatus.Stopped));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Start")]
        public async Task<string> StartAsync(int[] Ids)
        {
            return JsonHelper.ObjectToJSON(await _service.UpdateStatusByIdsAsync(Ids, (int)TaskInfoStatus.Running));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public async Task<string> DeleteAsync(int Id)
        {
            return JsonHelper.ObjectToJSON(await _service.DeleteAsync(Id));
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
                result.ResultMsg = ToErrorString(ModelState, "||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpGet]
        public async Task<string> IsExistsName([FromQuery]TaskInfoAddOrModifyModel item)
        {
            var result = await _service.IsExistsNameAsync(item);
            return JsonHelper.ObjectToJSON(result);
        }
    }
}