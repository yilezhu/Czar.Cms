using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using Czar.Cms.Admin.Filter;
using Czar.Cms.Admin.Validation;
using Czar.Cms.Core.Options;
using Czar.Cms.Repository.SqlServer;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using AutoMapper;
using Czar.Cms.Services;
using Czar.Cms.IServices;
using Czar.Cms.Quartz;
using Czar.Cms.ViewModels;
using NLog;

namespace Czar.Cms.Admin
{
    public class Startup
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            env.ConfigureNLog("Nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbOption>("CzarCms", Configuration.GetSection("DbOpion"));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Account/Index";
               options.LogoutPath = "/Account/Logout";
               options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
           });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
            });
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "AntiforgeryKey_yilezhu";
                options.HeaderName = "X-CSRF-TOKEN-yilezhu";
                options.SuppressXFrameOptionsHeader = false;
            });
            services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices()
                .AddFluentValidation(fv =>
                {
                    //程序集方式引入
                    fv.RegisterValidatorsFromAssemblyContaining<ManagerRoleValidation>();
                    //去掉其他的验证，只使用FluentValidation的验证规则
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            //DI了AutoMapper中需要用到的服务，其中包括AutoMapper的配置类 Profile
            services.AddAutoMapper();
            services.AddSingleton<ScheduleCenter>();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyTypes(typeof(ManagerRoleRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ManagerRoleService).Assembly)
                 .Where(t => t.Name.EndsWith("Service"))
                 .AsImplementedInterfaces();

            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory
            ,IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            try
            {
                var jobInfoAppService = app.ApplicationServices.GetRequiredService<ITaskInfoService>();
                var scheduleCenter = app.ApplicationServices.GetRequiredService<ScheduleCenter>();
                applicationLifetime.ApplicationStarted.Register(async () =>
                {
                    var list = await jobInfoAppService.GetListByJobStatuAsync((int)TaskInfoStatus.SystemStopped);
                    if (list?.Count() > 0)
                    {
                        list.ForEach(async x =>
                        {
                            await scheduleCenter.AddJobAsync(x.Name,
                                                    x.Group,
                                                    x.ClassName,
                                                    x.Assembly,
                                                    x.Cron);
                        });
                        await jobInfoAppService.ResumeSystemStoppedAsync();
                    }

                });
                applicationLifetime.ApplicationStopping.Register(async () =>
                {
                    var list = await jobInfoAppService.GetListByJobStatuAsync((int)TaskInfoStatus.Running);
                    if (list?.Count() > 0)
                    {
                        await jobInfoAppService.SystemStoppedAsync();
                        list.ForEach(async x =>
                        {
                            await scheduleCenter.DeleteJobAsync(x.Name, x.Group);
                        });
                    }


                });
            }
            catch (Exception ex)
            {

                logger.Error(ex, nameof(Startup));
            }
            
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
