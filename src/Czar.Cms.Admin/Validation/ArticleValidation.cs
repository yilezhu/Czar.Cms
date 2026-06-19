/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章输入验证                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/5/12 10:00:00                             
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.ViewModels;
using FluentValidation;
using System;

namespace Czar.Cms.Admin.Validation
{
    public class ArticleValidation : AbstractValidator<ArticleAddOrModifyModel>
    {
        public ArticleValidation()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("请选择文章分类");
            RuleFor(x => x.Title).NotEmpty().WithMessage("文章标题不能为空")
                .Length(2, 128).WithMessage("文章标题长度必须在2到128个字符之间");
            RuleFor(x => x.Sort).GreaterThanOrEqualTo(0).WithMessage("排序号必须大于等于0");
        }
    }
}
