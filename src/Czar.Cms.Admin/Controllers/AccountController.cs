using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Czar.Cms.Core.Helper;
using System.IO;
using Microsoft.AspNetCore.Http;
using Czar.Cms.ViewModels;
using Czar.Cms.IServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Czar.Cms.Admin.Validation;
using FluentValidation.Results;
using Czar.Cms.Core.Extensions;

namespace Czar.Cms.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly string CaptchaCodeSessionName = "CaptchaCode";
        private readonly string ManagerSignInErrorTimes = "ManagerSignInErrorTimes";
        private readonly int MaxErrorTimes = 3;
        private readonly IManagerService _service;

        public AccountController(IManagerService service)
        {
            _service = service;
        }

        public IActionResult Index(string returnUrl)
        {
            if (returnUrl.IsNullOrWhiteSpace())
            {
                returnUrl = "/";
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Route("Account/SignIn")]
        public async Task<string> SignInAsync(LoginModel model)
        {
            BaseResult result = new BaseResult();
            #region 判断验证码
            if (!ValidateCaptchaCode(model.CaptchaCode))
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInCaptchaCodeErrorCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInCaptchaCodeErrorMsg;
                return JsonHelper.ObjectToJSON(result);
            }
            #endregion
            #region 判断错误次数
            var ErrorTimes = HttpContext.Session.GetInt32(ManagerSignInErrorTimes);
            if (ErrorTimes == null)
            {
                HttpContext.Session.SetInt32(ManagerSignInErrorTimes, 1);
                ErrorTimes = 1;
            }
            else
            {
                HttpContext.Session.SetInt32(ManagerSignInErrorTimes, ErrorTimes.Value + 1);
            }
            if (ErrorTimes > MaxErrorTimes)
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInErrorTimesOverTimesCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInErrorTimesOverTimesMsg;
                return JsonHelper.ObjectToJSON(result);
            }
            #endregion
            #region 再次属性判断
            LoginModelValidation validation = new LoginModelValidation();
            ValidationResult results = validation.Validate(model);
            if (!results.IsValid)
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            #endregion

            model.Ip = HttpContext.GetClientUserIp();
            var manager = _service.SignIn(model);
            if (manager == null)
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInPasswordOrUserNameErrorCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInPasswordOrUserNameErrorMsg;
            }
            else if (manager.IsLock)
            {
                result.ResultCode = ResultCodeAddMsgKeys.SignInUserLockedCode;
                result.ResultMsg = ResultCodeAddMsgKeys.SignInUserLockedMsg;
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, manager.Email),
                    new Claim(ClaimTypes.MobilePhone, manager.Mobile),
                    new Claim(ClaimTypes.Role,manager.RoleId.ToString()),
                    new Claim("NickName",manager.NickName),
                    new Claim("LoginCount",manager.LoginCount.ToString()),
                    new Claim("LoginLastIp",manager.LoginLastIp),
                    new Claim("LoginLastTime",manager.LoginLastTime?.ToString("yyyy-MM-dd HH:mm:ss")),
                };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.
                    IsPersistent = true,
                    // 验证会话是否在多个请求之间持久化。设置用addcookie设置的cookieAuthenticationOptions的ExpireTimeSpan选项时必需。设置expiresUTC时也需要。

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
                    // 身份验证票证过期的时间。此处设置的值将覆盖用addcookie设置的cookieAuthenticationOptions的ExpireTimeSpan选项。

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
            return JsonHelper.ObjectToJSON(result);
        }


        [HttpPost, ValidateAntiForgeryToken, Route("Account/SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        public IActionResult GetCaptchaImage()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            HttpContext.Session.SetString(CaptchaCodeSessionName, captchaCode);
            return new FileStreamResult(new MemoryStream(result.CaptchaByteData), "image/png");
        }

        private bool ValidateCaptchaCode(string userInputCaptcha)
        {
            var isValid = userInputCaptcha == HttpContext.Session.GetString(CaptchaCodeSessionName);
            HttpContext.Session.Remove(CaptchaCodeSessionName);
            return isValid;
        }
    }
}