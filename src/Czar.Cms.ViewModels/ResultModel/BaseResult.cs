/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：结果基类                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/29 17:30:51                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Admin.ResultModel                                 
*│　类    名： BaseResult                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czar.Cms.ViewModels
{
    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int ResultCode { get; set; } = ResultCodeAddMsgKeys.CommonObjectSuccessCode;
        /// <summary>
        /// 结果消息 如果不成功，返回的错误信息
        /// </summary>
        public string ResultMsg { get; set; } = ResultCodeAddMsgKeys.CommonObjectSuccessMsg;
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BaseResult()
        {

        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="resultCode">结果代码</param>
        /// <param name="resultMsg">结果信息</param>
        public BaseResult(int resultCode, string resultMsg)
        {
            ResultCode = resultCode;
            ResultMsg = resultMsg;
        }
    }
}
