/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：权限校验过滤器                                                         
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/24 15:47:45                             
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Admin.Filter
{
    /// <summary>
    /// 权限校验过滤器：根据登录用户的角色，校验当前请求的 Controller/Action 是否在允许的菜单中。
    /// 白名单：Account（登录/登出）、Home（首页/主框架）、File（上传）、以及带有 [AllowAnonymous] 的 Action。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        private static readonly HashSet<string> WhiteListControllers = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Account", "Home", "File"
        };

        /// <summary>
        /// 权限缓存的有效期（秒）—— 避免每次请求都查数据库
        /// </summary>
        private const int PermissionCacheSeconds = 600;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var routeData = context.RouteData;
            var user = httpContext.User;

            // 1. AllowAnonymous 直接放行
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m is AllowAnonymousAttribute))
            {
                await Task.CompletedTask;
                return;
            }

            // 2. 未登录 -> 跳转到登录页
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new
                {
                    controller = "Account",
                    action = "Index",
                    area = ""
                });
                return;
            }

            // 3. 白名单控制器直接放行（首页、登录、文件上传等基础功能）
            var controllerName = (routeData.Values["controller"] ?? "").ToString();
            if (WhiteListControllers.Contains(controllerName))
            {
                return;
            }

            // 4. 获取登录用户的角色ID
            var roleIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int roleId))
            {
                // 角色信息异常，跳转登录
                context.Result = new RedirectToRouteResult(new { controller = "Account", action = "Index", area = "" });
                return;
            }

            // 5. 从缓存或数据库获取该角色的所有可访问菜单路径
            var menuService = (IMenuService)httpContext.RequestServices.GetService(typeof(IMenuService));
            var cache = (IMemoryCache)httpContext.RequestServices.GetService(typeof(IMemoryCache));
            if (menuService == null)
            {
                // 开发阶段未注册 IMenuservice，直接放行
                return;
            }

            var allowedPaths = cache != null
                ? await cache.GetOrCreateAsync($"PERMISSION_{roleId}", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(PermissionCacheSeconds);
                    var menuList = await menuService.GetMenusByRoleIdAsync(roleId);
                    return menuList?.Where(m => !string.IsNullOrWhiteSpace(m.LinkUrl))
                                     .Select(m => NormalizePath(m.LinkUrl))
                                     .ToHashSet(StringComparer.OrdinalIgnoreCase)
                            ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                })
                : (await menuService.GetMenusByRoleIdAsync(roleId))
                    ?.Where(m => !string.IsNullOrWhiteSpace(m.LinkUrl))
                     .Select(m => NormalizePath(m.LinkUrl))
                     .ToHashSet(StringComparer.OrdinalIgnoreCase)
                  ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // 6. 当前请求路径匹配
            var actionName = (routeData.Values["action"] ?? "").ToString();
            var currentPath = $"/{controllerName}/{actionName}".Trim();

            // 7. 判定：当前控制器主路径 /Controller 也视为匹配
            if (!allowedPaths.Contains(currentPath) &&
                !allowedPaths.Contains($"/{controllerName}"))
            {
                // 拒绝访问 —— AJAX 请求返回 JSON 403，其他请求返回跳转首页
                var isAjax = string.Equals(httpContext.Request.Headers["X-Requested-With"], "XMLHttpRequest",
                               StringComparison.OrdinalIgnoreCase);
                if (isAjax)
                {
                    context.Result = new ContentResult
                    {
                        Content = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            ResultCode = 403,
                            ResultMsg = "您没有权限执行此操作"
                        }),
                        ContentType = "application/json",
                        StatusCode = 403
                    };
                }
                else
                {
                    context.Result = new RedirectToRouteResult(new { controller = "Home", action = "Index", area = "" });
                }
            }
        }

        /// <summary>
        /// 将菜单中的 LinkUrl 标准化为 /Controller/Action 格式（去头尾空格、转小写、补 /）
        /// </summary>
        private static string NormalizePath(string linkUrl)
        {
            if (string.IsNullOrWhiteSpace(linkUrl)) return string.Empty;
            var trimmed = linkUrl.Trim();
            if (!trimmed.StartsWith("/", StringComparison.Ordinal))
                trimmed = "/" + trimmed;
            return trimmed;
        }
    }
}
