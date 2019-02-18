
using Czar.Cms.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.Core.Extensions;

namespace Czar.Cms.Admin.Validation
{
    public class ManagerValidation : AbstractValidator<ManagerAddOrModifyModel>
    {
        public ManagerValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.UserName).NotEmpty().Length(5, 32).WithMessage("登陆ID不能为空并且长度不能超过32个字符");
            RuleFor(x => x.RoleId).NotNull().WithMessage("用户所属角色不能为空") ;
            RuleFor(x => x.NickName).Length(1,32).WithMessage("用户昵称长度不能超过32个字符") ;
            RuleFor(x => x.Mobile).Must(IsMobile).WithMessage("手机号码格式不正确");
            RuleFor(x => x.Email).Must(IsEmail).WithMessage("邮箱地址格式不正确");

            RuleFor(x => x.Remark).Length(0, 128).WithMessage("备注信息的长度必须符合规则");
        }

        private bool IsEmail(string arg)
        {
            if (arg.IsNullOrWhiteSpace())
            {
                return true;
            }
            if (arg.IsEmail())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsMobile(string arg)
        {
            if (arg.IsNullOrWhiteSpace())
            {
                return true;
            }
            if (arg.IsMobileNumber())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
