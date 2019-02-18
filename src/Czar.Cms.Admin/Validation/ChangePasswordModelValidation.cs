
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
            RuleFor(x => x.NewPassword).NotEmpty().Length(4, 32).WithMessage("新密码不能为空且长度必须符合规则")
                .Must(NewNotEqualsOld).WithMessage("新密码不能跟旧密码一样");
            RuleFor(x => x.NewPasswordRe).NotEmpty().WithMessage("重复密码不能为空").Must(ReEqualsNew).WithMessage("重复密码必须跟新密码一样");

        }

        /// <summary>
        /// 判断新旧密码是否一样
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>结果</returns>
        private bool NewNotEqualsOld(ChangePasswordModel model , string newPwd)
        {
            return model.OldPassword!=newPwd;
        }

        /// <summary>
        /// 判断新密码与重复密码是否一样
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newPwdRe"></param>
        /// <returns></returns>
        private bool ReEqualsNew(ChangePasswordModel model, string newPwdRe)
        {
            return model.NewPassword == newPwdRe;
        }
    }
}
