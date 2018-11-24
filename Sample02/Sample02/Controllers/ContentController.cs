using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Sample01.Models;

namespace Sample01.Controllers
{
    /// <summary>
    /// 2018.11.19
    /// 祝雷
    /// Content控制器
    /// </summary>
    public class ContentController : Controller
    {
        private readonly Content contents;
        public ContentController(IOptions<Content> option)
        {
            contents = option.Value;
        }
        /// <summary>
        /// 首页显示
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
           
            return View(new ContentViewModel { Contents=new List<Content> { contents} });
        }
    }
}