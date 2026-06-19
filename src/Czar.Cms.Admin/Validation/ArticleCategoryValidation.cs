/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类输入验证                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.ViewModels;
using FluentValidation;
using System;

namespace Czar.Cms.Admin.Validation
{
    public class ArticleCategoryValidation : AbstractValidator<ArticleCategoryAddOrModifyModel>
    {
        public ArticleCategoryValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Title).NotEmpty().WithMessage("分类名称不能为空")
                .Length(2, 128).WithMessage("分类名称长度必须在2到128个字符之间");

            RuleFor(x => x.ParentId).GreaterThanOrEqualTo(0).WithMessage("父分类ID不合法");

            RuleFor(x => x.Sort).GreaterThanOrEqualTo(0).WithMessage("排序号必须大于等于0");

            RuleFor(x => x.SeoTitle).MaximumLength(128).WithMessage("SEO标题长度不能超过128个字符");
        }
    }
}
