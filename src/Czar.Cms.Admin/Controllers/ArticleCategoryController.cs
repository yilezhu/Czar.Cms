/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章分类控制器                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/7 16:50:56                             
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Admin.Validation;
using Czar.Cms.Core.Helper;
using Czar.Cms.IServices;
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Controllers
{
    public class ArticleCategoryController : BaseController
    {
        private readonly IArticleCategoryService _service;

        public ArticleCategoryController(IArticleCategoryService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("LoadData")]
        public async Task<string> LoadDataAsync([FromQuery] ArticleCategoryRequestModel model)
        {
            return JsonHelper.ObjectToJSON(await _service.LoadDataAsync(model));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrModify(int id)
        {
            var model = new ArticleCategoryAddOrModifyModel();
            if (id > 0)
            {
                var list = await _service.GetAllListAsync();
                var dbItem = list.FirstOrDefault(p => p.Id == id);
                if (dbItem != null)
                {
                    model.Id = dbItem.Id;
                    model.Title = dbItem.Title;
                    model.ParentId = dbItem.ParentId;
                    model.Sort = dbItem.Sort;
                    model.ImageUrl = dbItem.ImageUrl;
                    model.SeoTitle = dbItem.SeoTitle;
                    model.SeoKeywords = dbItem.SeoKeywords;
                    model.SeoDescription = dbItem.SeoDescription;
                }
            }
            return View(model);
        }

        [HttpPost, ActionName("AddOrModify")]
        [ValidateAntiForgeryToken]
        public async Task<string> AddOrModifyAsync([FromForm] ArticleCategoryAddOrModifyModel item)
        {
            var result = new BaseResult();
            ArticleCategoryValidation validationRules = new ArticleCategoryValidation();
            ValidationResult results = validationRules.Validate(item);
            if (results.IsValid)
            {
                result = await _service.AddOrModifyAsync(item);
            }
            else
            {
                result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
                result.ResultMsg = results.ToString("||");
            }
            return JsonHelper.ObjectToJSON(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<string> DeleteAsync(int[] ids)
        {
            return JsonHelper.ObjectToJSON(await _service.DeleteIdsAsync(ids));
        }
    }
}
