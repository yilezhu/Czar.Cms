
using Czar.Cms.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.Core.Extensions;

namespace Czar.Cms.Admin.Validation
{
    public class ManagerLockStatusModelValidation : AbstractValidator<ChangeStatusModel>
    {
        public ManagerLockStatusModelValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("主键不能为空");
            RuleFor(x => x.Status).NotNull().WithMessage("状态不能为空") ;
        }
    }
}
