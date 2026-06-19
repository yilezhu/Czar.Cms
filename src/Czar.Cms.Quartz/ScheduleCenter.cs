/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：任务调度中心
*│　作    者：yilezhu
*│　版    本：1.0
*│　创建时间：2019/3/13 10:15:45
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Quartz
*│　类    名： ScheduleCenter
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.ViewModels;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;

namespace Czar.Cms.Quartz
{
    public class ScheduleCenter : IDisposable
    {
        private readonly ILogger<ScheduleCenter> _logger;
        private readonly IScheduler _scheduler;

        public ScheduleCenter(ILogger<ScheduleCenter> logger)
        {
            _logger = logger;
            NameValueCollection parms = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "TestScheduler",
                ["quartz.serializer.type"] = "binary",
                ["quartz.threadPool.threadCount"] = "20",
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(parms);
            _scheduler = factory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.Start();
        }

        /// <summary>
        /// 添加调度任务
        /// </summary>
        public async Task<ScheduleResult> AddJobAsync(string jobName, string jobGroup, string jobNamespaceAndClassName, string jobAssemblyName, string cronExpress)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                if (string.IsNullOrEmpty(jobName) || string.IsNullOrEmpty(jobGroup)
                    || string.IsNullOrEmpty(jobNamespaceAndClassName) || string.IsNullOrEmpty(jobAssemblyName)
                    || string.IsNullOrEmpty(cronExpress))
                {
                    result.ResultCode = -3;
                    result.ResultMsg = "参数不能为空";
                    return result;
                }

                var jobKey = new JobKey(jobName, jobGroup);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.PauseJob(jobKey);
                    await _scheduler.DeleteJob(jobKey);
                }

                Assembly assembly = Assembly.Load(new AssemblyName(jobAssemblyName));
                Type jobType = assembly.GetType(jobNamespaceAndClassName);
                if (jobType == null)
                {
                    result.ResultCode = -1;
                    result.ResultMsg = "系统找不到对应的任务，请重新设置";
                    return result;
                }

                IJobDetail job = JobBuilder.Create(jobType)
                    .WithIdentity(jobKey)
                    .UsingJobData("ServerName", _scheduler.SchedulerName)
                    .Build();

                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                    .StartAt(DateTime.Now)
                    .EndAt(DateBuilder.NextGivenSecondDate(DateTime.MaxValue.AddDays(-1), 1))
                    .WithIdentity(jobName, jobGroup)
                    .WithCronSchedule(cronExpress)
                    .Build();

                await _scheduler.ScheduleJob(job, trigger);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(AddJobAsync));
                result.ResultCode = -4;
                result.ResultMsg = ex.ToString();
                return result;
            }
        }

        /// <summary>
        /// 暂停指定任务计划
        /// </summary>
        public async Task<ScheduleResult> StopJobAsync(string jobName, string jobGroup)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.PauseJob(jobKey);
                }
                else
                {
                    result.ResultCode = -1;
                    result.ResultMsg = "任务不存在";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(StopJobAsync));
                result.ResultCode = -4;
                result.ResultMsg = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// 恢复指定的任务计划，如果是程序崩溃后或进程被杀后的恢复，此方法无效
        /// </summary>
        public async Task<ScheduleResult> ResumeJobAsync(string jobName, string jobGroup)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.ResumeJob(jobKey);
                }
                else
                {
                    result.ResultCode = -1;
                    result.ResultMsg = "任务不存在";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(ResumeJobAsync));
                result.ResultCode = -4;
                result.ResultMsg = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// 删除指定的任务
        /// </summary>
        public async Task<ScheduleResult> DeleteJobAsync(string jobName, string jobGroup)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.PauseJob(jobKey);
                    await _scheduler.DeleteJob(jobKey);
                }
                else
                {
                    result.ResultCode = -1;
                    result.ResultMsg = "任务不存在";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(DeleteJobAsync));
                result.ResultCode = -4;
                result.ResultMsg = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// 释放调度器资源
        /// </summary>
        public void Dispose()
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                _scheduler.Shutdown(waitForJobsToComplete: true);
            }
        }
    }
}
