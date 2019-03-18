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
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.Job
{
    public class LogTestJob : IJob
    {
        
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string serverName = dataMap.GetString("ServerName");
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = "kong";
            }
            //实例化一个文件流--->与写入文件相关联
            var filepath = AppDomain.CurrentDomain.BaseDirectory+DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            using (var fs = new FileStream(filepath+ "\\testLog.txt", FileMode.Append, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    await sw.WriteLineAsync($"Hello, {serverName},at {DateTime.Now.ToString("")}");
                    //清空缓冲区
                    await sw.FlushAsync();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }



            await Task.CompletedTask;
        }
    }
}
