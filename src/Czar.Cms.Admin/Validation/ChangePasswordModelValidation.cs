
using Czar.Cms.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.Core.Extensions;

namespace Czar.Cms.Admin.Validation
{
    public class ChangePasswordModelValidation : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("用户信息获取异常");
            RuleFor(x => x.OldPassword).NotEmpty().Length(4, 32).WithMessage("旧密码不能为空且长度必须符合规则");
            RuleFor(x => x.NewPassword).NotEmpty().Length(4, 32).WithMessage("旧密码不能为空且长度必须符合规则")
                .Must(NewNotEqualsOld).WithMessage("新密码不能跟旧密码一样");
            RuleFor(x => x.NewPasswordRe).NotEmpty().Must(ReEqualsNew).WithMessage("新密码不能跟旧密码一样");

        }
        private bool NewNotEqualsOld(ChangePasswordModel model , string newPwd)
        {
            return model.OldPassword!=newPwd;
        }

        private bool ReEqualsNew(ChangePasswordModel model, string newPwdRe)
        {
            return model.NewPassword == newPwdRe;
        }
    }
}
