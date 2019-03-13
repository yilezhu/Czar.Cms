/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/13 15:27:15                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.TaskInfo                                   
*│　类    名： TaskStatus                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public enum TaskInfoStatus:byte
    {
        [Description("执行中")]
        Running,
        [Description("已完成")]
        Completed,
        [Description("已停止")]
        Stopped,
        [Description("系统停止")]
        SystemStopped,
    }
}
