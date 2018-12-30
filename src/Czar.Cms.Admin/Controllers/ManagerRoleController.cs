using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Czar.Cms.Models;
using Czar.Cms.IRepository;
using Czar.Cms.Admin.ResultModel;
using System.Text;

namespace Czar.Cms.Admin.Controllers
{
    public class ManagerRoleController : BaseController
    {
        private readonly IManagerRoleRepository _managerRoleRepository;

        public ManagerRoleController(IManagerRoleRepository managerRoleRepository)
        {
            _managerRoleRepository = managerRoleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddOrModify()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrModify([FromForm]ManagerRole item)
        {
            var result = new BaseResult();
            if (ModelState.IsValid)
            {
                item.IsDelete = false;
                if (item.Id == 0)
                {
                    //TODO ADD
                    item.AddManagerId = 1;
                    item.AddTime = DateTime.Now;
                    if (_managerRoleRepository.Insert(item) > 0)
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;

                    }
                    else
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                    }
                }
                else
                {
                    //TODO Modify
                    item.ModifyManagerId = 1;
                    item.ModifyTime = DateTime.Now;
                    if (_managerRoleRepository.Update(item) > 0)
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
                    }
                    else
                    {
                        result.ResultCode = ResultCodeAddMsgKeys.CommonExceptionCode;
                        result.ResultMsg = ResultCodeAddMsgKeys.CommonExceptionMsg;
                    }

                }
            }
            else
            {
                StringBuilder errinfo = new StringBuilder();
                foreach (var s in ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {
                        errinfo.AppendFormat("{0}||", p.ErrorMessage);
                    }
                }
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = errinfo.ToString();
            }
            return Json(result);
        }
    }
}