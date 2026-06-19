using Czar.Cms.ViewModels;
using FluentValidation;
using Quartz;
using System;
using System.Linq;

namespace Czar.Cms.Admin.Validation
{
    public class TaskInfoValidation : AbstractValidator<TaskInfoAddOrModifyModel>
    {
        public TaskInfoValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("任务别名不能为空")
                .Length(2, 64).WithMessage("任务别名长度必须在2-64个字符之间");

            RuleFor(x => x.Group)
                .Length(0, 64).WithMessage("任务分组长度不能超过64个字符");

            RuleFor(x => x.Assembly)
                .NotEmpty().WithMessage("程序集名称不能为空")
                .Length(6, 256).WithMessage("程序集长度不能超过256个字符");

            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("完整类名不能为空")
                .Length(2, 256).WithMessage("完整类名长度不能超过256个字符");

            RuleFor(x => x.Cron)
                .NotEmpty().WithMessage("Cron表达式不能为空")
                .Length(2, 128).WithMessage("Cron表达式长度不能超过128个字符")
                .Must(BeValidCronExpression).WithMessage("Cron表达式格式不正确");
        }

        private static bool BeValidCronExpression(string cronExpression)
        {
            if (string.IsNullOrWhiteSpace(cronExpression))
                return false;
            return CronExpression.IsValidExpression(cronExpression);
        }
    }
}
