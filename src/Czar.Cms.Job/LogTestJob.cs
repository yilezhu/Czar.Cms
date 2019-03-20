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
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Job
{
    public class LogTestJob : IJob
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

     
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string serverName = dataMap.GetString("ServerName");
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = "kong";
            }
            logger.Error($"Hello, {serverName},at {DateTime.Now.ToString()}");
            await Task.CompletedTask;
        }
    }
}
