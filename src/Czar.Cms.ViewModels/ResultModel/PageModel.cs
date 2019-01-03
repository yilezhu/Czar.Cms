/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：分页相关实体                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/30 16:02:05                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels                                   
*│　类    名： PageModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class PageModel
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public string Key { get; set; }

        public PageModel()
        {
            Page = 1;
            Limit = 10;
        }
    }
}
