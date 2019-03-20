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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Quartz
{
    public class ScheduleCenter
    {
        private readonly ILogger<ScheduleCenter> _logger;
        private readonly object Locker = new object();
        /// <summary>
        /// 任务计划
        /// </summary>
        private IScheduler Scheduler;


        public ScheduleCenter(ILogger<ScheduleCenter> logger)
        {
            _logger = logger;
            NameValueCollection parms = new NameValueCollection
            {
                ////scheduler名字
                ["quartz.scheduler.instanceName"] = "TestScheduler",
                //序列化类型
                ["quartz.serializer.type"] = "binary",//json,切换为数据库存储的时候需要设置json
                ////自动生成scheduler实例ID，主要为了保证集群中的实例具有唯一标识
                //["quartz.scheduler.instanceId"] = "AUTO",
                ////是否配置集群
                //["quartz.jobStore.clustered"] = "true",
                ////线程池个数
                ["quartz.threadPool.threadCount"] = "20",
                ////类型为JobStoreXT,事务
                //["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
                ////以下配置需要数据库表配合使用，表结构sql地址：https://github.com/quartznet/quartznet/tree/master/database/tables
                ////JobDataMap中的数据都是字符串
                ////["quartz.jobStore.useProperties"] = "true",
                ////数据源名称
                //["quartz.jobStore.dataSource"] = "mySS",
                ////数据表名前缀
                //["quartz.jobStore.tablePrefix"] = "QRTZ_",
                ////使用Sqlserver的Ado操作代理类
                //["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                ////数据源连接字符串
                //["quartz.dataSource.mySS.connectionString"] = "Server=localhost;Database=CzarAbpDemo;User Id = sa;Password = 1;Trusted_Connection=True;MultipleActiveResultSets=true",
                ////数据源的数据库
                //["quartz.dataSource.mySS.provider"] = "SqlServer"
            };
            // 从Factory中获取Scheduler实例
            StdSchedulerFactory factory = new StdSchedulerFactory(parms);
            Scheduler = factory.GetScheduler().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 添加调度任务
        /// </summary>
        /// <param name="JobName">任务名称</param>
        /// <param name="JobGroup">任务分组</param>
        /// <param name="JobNamespaceAndClassName">任务完全限定名</param>
        /// <param name="JobAssemblyName">任务程序集名称</param>
        /// <param name="CronExpress">Cron表达式</param>
        /// <param name="StarTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public async Task<ScheduleResult> AddJobAsync(String JobName, String JobGroup, String JobNamespaceAndClassName, String JobAssemblyName, string CronExpress)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                if (string.IsNullOrEmpty(JobName) || string.IsNullOrEmpty(JobGroup) || string.IsNullOrEmpty(JobNamespaceAndClassName) || string.IsNullOrEmpty(JobAssemblyName) || string.IsNullOrEmpty(CronExpress))
                {
                    result.ResultCode = -3;
                    result.ResultMsg = $"参数不能为空";
                    return result;//出现异常
                }
                var starRunTime = DateTime.Now;
                var EndTime = DateTime.MaxValue.AddDays(-1);
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
                JobKey jobKey = new JobKey(JobName, JobGroup);
                if (await Scheduler.CheckExists(jobKey))
                {
                    await Scheduler.PauseJob(jobKey);
                    await Scheduler.DeleteJob(jobKey);
                }
                Assembly assembly = Assembly.LoadFile(JobAssemblyName);
                Type jobType = assembly.GetType(JobNamespaceAndClassName);
                //var jobType = Type.GetType(JobNamespaceAndClassName + "," + JobAssemblyName);
                if (jobType == null)
                {
                    result.ResultCode = -1;
                    result.ResultMsg = "系统找不到对应的任务，请重新设置";
                    return result;//出现异常
                }
                IJobDetail job = JobBuilder.Create(jobType)
                .WithIdentity(jobKey).UsingJobData("ServerName", Scheduler.SchedulerName)
                .Build();
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                             .StartAt(starRunTime)
                                             .EndAt(endRunTime)
                                             .WithIdentity(JobName, JobGroup)
                                             .WithCronSchedule(CronExpress)
                                             .Build();
                await Scheduler.ScheduleJob(job, trigger);
                if (!Scheduler.IsStarted)
                {
                    await Scheduler.Start();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(AddJobAsync));
                result.ResultCode = -4;
                result.ResultMsg = ex.ToString();
                return result;//出现异常
            }
        }


        /// <summary>
        /// 暂停指定任务计划
        /// </summary>
        /// <param name="jobName">任务名</param>
        /// <param name="jobGroup">任务分组</param>
        /// <returns></returns>
        public async Task<ScheduleResult> StopJobAsync(string jobName, string jobGroup)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                JobKey jobKey = new JobKey(jobName, jobGroup);
                if (await Scheduler.CheckExists(jobKey))
                {
                    await Scheduler.PauseJob(new JobKey(jobName, jobGroup));
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
            return result;//出现异常
        }

        /// <summary>
        /// 恢复指定的任务计划,如果是程序奔溃后 或者是进程杀死后的恢复，此方法无效
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroup">任务组</param>
        /// <returns></returns>
        public async Task<ScheduleResult> ResumeJobAsync(string jobName, string jobGroup)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                JobKey jobKey = new JobKey(jobName, jobGroup);

                if (await Scheduler.CheckExists(jobKey))
                {
                    //resumejob 恢复
                    await Scheduler.PauseJob(jobKey);
                    await Scheduler.ResumeJob(jobKey);
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
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroup">任务组</param>
        /// <returns></returns>
        public async Task<ScheduleResult> DeleteJobAsync(string jobName, string jobGroup)
        {
            ScheduleResult result = new ScheduleResult();
            try
            {
                JobKey jobKey = new JobKey(jobName, jobGroup);

                if (await Scheduler.CheckExists(jobKey))
                {
                    //先暂停，再移除
                    await Scheduler.PauseJob(jobKey);
                    await Scheduler.DeleteJob(jobKey);
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
    }
}
