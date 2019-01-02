
using Czar.Cms.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Czar.Cms.Core.Extensions;

namespace Czar.Cms.Admin.Validation
{
    public class ManagerLockStatusModelValidation : AbstractValidator<ManagerChangeLockStatusModel>
    {
        public ManagerLockStatusModelValidation()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("主键不能为空");
            RuleFor(x => x.IsLock).NotNull().WithMessage("是否锁定状态不能为空") ;
        }
    }
}
