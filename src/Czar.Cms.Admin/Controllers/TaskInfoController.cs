using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.Admin.Validation;
using Czar.Cms.Core.Helper;
using Czar.Cms.IServices;
using Czar.Cms.Quartz;
using Czar.Cms.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Czar.Cms.Admin.Controllers
{
    public class TaskInfoController : BaseController
    {
        private readonly ITaskInfoService _service;
        private readonly ScheduleCenter _scheduleCenter;

        public TaskInfoController(ITaskInfoService service, ScheduleCenter scheduleCenter)
        {
            _service = service;
            _scheduleCenter = scheduleCenter;
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
        [Route("/TaskInfo/Stop/")]
        public async Task<string> StopAsync(int[] Ids)
        {
            BooleanResult result = new BooleanResult();
            var list = await _service.GetListByIdsAsync(Ids);
            if (list?.Count > 0)
            {
                list.ForEach(async x =>
                {
                    await _scheduleCenter.StopJobAsync(x.Name,x.Group);
                });
                result= await _service.UpdateStatusByIdsAsync(Ids, (int)TaskInfoStatus.Stopped);
            }
            
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/TaskInfo/Start/")]
        public async Task<string> StartAsync(int[] Ids)
        {
            BooleanResult result = new BooleanResult();

            var list = await _service.GetListByIdsAsync(Ids);
            if (list?.Count > 0)
            {
                list.ForEach(async x =>
                {
                    await _scheduleCenter.AddJobAsync(x.Name, x.Group,x.ClassName,x.Assembly,x.Cron);
                });
                result = await _service.UpdateStatusByIdsAsync(Ids, (int)TaskInfoStatus.Running);
            }
            return JsonHelper.ObjectToJSON(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/TaskInfo/Delete/")]
        public async Task<string> DeleteAsync(int Id)
        {
            BooleanResult result = new BooleanResult();

            var item = await _service.GetByIdAsync(Id); 
            if (item == null)
            {
                result.Data = false;
            }
            else
            {
                await _scheduleCenter.DeleteJobAsync(item.Name,item.Group);
                result = await _service.DeleteAsync(Id);
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/TaskInfo/ChangeStatus/")]
        public async Task<string> ChangeStatusAsync([FromForm]ChangeStatusModel item)
        {
            var result = new BooleanResult();

            if (ModelState.IsValid)
            {
                var model = await _service.GetByIdAsync(item.Id);
                if (model == null)
                {
                    result.Data = false;
                }
                else {
                    int[] ids = { item.Id };
                    if (item.Status)
                    {
                        await _scheduleCenter.AddJobAsync(model.Name, model.Group, model.ClassName, model.Assembly, model.Cron);

                        result = await _service.UpdateStatusByIdsAsync(ids, (int)TaskInfoStatus.Running);
                    }
                    else
                    {
                        await _scheduleCenter.StopJobAsync(model.Name, model.Group);

                        result = await _service.UpdateStatusByIdsAsync(ids, (int)TaskInfoStatus.Stopped);
                    }
                }
               
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = ToErrorString(ModelState, "||");
                result.Data = false;
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