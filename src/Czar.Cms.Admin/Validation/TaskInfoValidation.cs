
using Czar.Cms.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Validation
{
    public class TaskInfoValidation : AbstractValidator<TaskInfoAddOrModifyModel>
    {
        public TaskInfoValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).Length(2, 64).WithMessage("任务别名不能为空且最大长度不能超过64个字符");
            RuleFor(x => x.Group).Length(2, 64).WithMessage("任务分组长度不能超过64个字符");
            RuleFor(x => x.Assembly).Length(6, 256).WithMessage("程序集长度不能超过256个字符");
            RuleFor(x => x.ClassName).Length(2, 256).WithMessage("完整类名不能超过256个字符") ;
            RuleFor(x => x.Cron).Length(2, 128).WithMessage("Cron表达式不能超过128个字符");

        }
    }
}
