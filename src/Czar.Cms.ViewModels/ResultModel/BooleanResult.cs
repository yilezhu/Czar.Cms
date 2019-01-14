/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：布尔类型的结果                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/8 22:14:26                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.ViewModels.ResultModel                                   
*│　类    名： BooleanResult                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.ViewModels
{
    public class BooleanResult:BaseResult
    {
        public Boolean Data { get; set; }
    }
}
