using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Czar.Cms.Core.Helper;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Czar.Cms.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly string CaptchaCodeSessionName = "CaptchaCode";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCaptchaImage()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            HttpContext.Session.SetString(CaptchaCodeSessionName, captchaCode);
            return new FileStreamResult(new MemoryStream(result.CaptchaByteData), "image/png");
        }

        public bool ValidateCaptchaCode(string userInputCaptcha)
        {
            var isValid = userInputCaptcha == HttpContext.Session.GetString(CaptchaCodeSessionName);
            HttpContext.Session.Remove(CaptchaCodeSessionName);
            return isValid;
        }
    }
}