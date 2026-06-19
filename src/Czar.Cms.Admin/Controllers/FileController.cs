/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文件上传控制器
*│　作    者：yilezhu
*│　版    本：1.0
*│　创建时间：2019/1/24 15:47:45
*└──────────────────────────────────────────────────────────────┘
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Controllers
{
    public class FileController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnv;

        /// <summary>
        /// 允许上传的图片后缀
        /// </summary>
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        public FileController(IWebHostEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }

        /// <summary>
        /// 图片上传功能
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            var imgFile = Request.Form.Files.FirstOrDefault();
            if (imgFile == null || string.IsNullOrEmpty(imgFile.FileName))
            {
                return Json(new { code = 1, msg = "请选择要上传的文件。" });
            }

            // 解析文件名和后缀
            var contentDisposition = ContentDispositionHeaderValue.Parse(imgFile.ContentDisposition);
            var fileName = contentDisposition.FileName.Trim('"');
            var extName = Path.GetExtension(fileName).ToLowerInvariant();

            // 1. 校验文件类型
            if (!AllowedImageExtensions.Contains(extName))
            {
                return Json(new { code = 1, msg = $"只允许上传图片格式：{string.Join(",", AllowedImageExtensions)}。" });
            }

            // 2. 校验文件大小（限制 1MB）
            long mb = imgFile.Length / 1024 / 1024;
            if (mb > 1)
            {
                return Json(new { code = 1, msg = "只允许上传小于 1MB 的图片。" });
            }

            // 3. 生成文件名与目录（跨平台路径）
            var dir = DateTime.Now.ToString("yyyyMMdd");
            var newFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}{Guid.NewGuid():N}{extName}";
            var uploadDir = Path.Combine(_hostEnv.WebRootPath, "upload", dir);
            var targetPath = Path.Combine(uploadDir, newFileName);

            // 4. 创建目录并写入文件
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            await using (var fs = new FileStream(targetPath, FileMode.Create))
            {
                await imgFile.CopyToAsync(fs);
                await fs.FlushAsync();
            }

            return Json(new
            {
                code = 0,
                msg = "上传成功",
                data = new
                {
                    src = $"/upload/{dir}/{newFileName}",
                    title = Path.GetFileNameWithoutExtension(fileName)
                }
            });
        }
    }
}
