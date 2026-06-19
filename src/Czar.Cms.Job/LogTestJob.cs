/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：
*│　作    者：yilezhu
*│　版    本：1.0
*│　创建时间：2019/3/18 15:36:03
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Job
*│　类    名： LogTestJob
*└──────────────────────────────────────────────────────────────┘
*/
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Czar.Cms.Job
{
    public class LogTestJob : IJob
    {
        private readonly ILogger<LogTestJob> _logger;

        public LogTestJob(ILogger<LogTestJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string serverName = dataMap.GetString("ServerName");
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = "unknown";
            }
            _logger.LogError("Hello, {ServerName}, at {Time}", serverName, DateTime.Now);
            await Task.CompletedTask;
        }
    }
}
