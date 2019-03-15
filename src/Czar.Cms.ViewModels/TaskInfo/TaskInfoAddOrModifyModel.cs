/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：新增或者编辑实体                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/14 10:08:08                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.TaskInfo                                   
*│　类    名： TaskInfoAddOrModifyModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class TaskInfoAddOrModifyModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Group { get; set; }
        public String Description { get; set; }
        public String Assembly { get; set; }
        public String ClassName { get; set; }
        public String Cron { get; set; }
        public DateTime? AddTime { get; set; }
        public int AddManagerId { get; set; }
    }
}
