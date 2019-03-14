/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：页面显示的实体                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/13 20:27:30                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.TaskInfo                                   
*│　类    名： TaskInfoDto                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class TaskInfoDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Group { get; set; }

        public String Description { get; set; }

        public String Assembly { get; set; }

        public String ClassName { get; set; }

        public Int32 Status { get; set; }

        public String Cron { get; set; }

    }
}
