using Czar.Cms.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Validation
{
    public class LoginModelValidation : AbstractValidator<LoginModel>
    {
        public LoginModelValidation()
        {
            RuleFor(x => x.UserName).Length(4,32).WithMessage("用户名不能为空且必须符合规则");
            RuleFor(x => x.Password).Length(4,32).WithMessage("密码不能为空且必须符合规则");
            RuleFor(x => x.CaptchaCode).Length(4).WithMessage("请正确输入四位验证码");

        }
    }
}
