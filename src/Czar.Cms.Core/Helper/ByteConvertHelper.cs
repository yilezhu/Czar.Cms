/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：Byt转换操作类,依赖JsonHelper类                                                 
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/1 21:41:20                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Core.Helper                                   
*│　类    名： ByteConvertHelper                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.Core.Helper
{
    public class ByteConvertHelper
    {
        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] Object2Bytes(object obj)
        {
            byte[] serializedResult = Encoding.UTF8.GetBytes(JsonHelper.ObjectToJSON(obj));
            return serializedResult;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static object Bytes2Object(byte[] buff)
        {
            return JsonHelper.JSONToObject<object>(Encoding.UTF8.GetString(buff));
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static T Bytes2Object<T>(byte[] buff)
        {
            return JsonHelper.JSONToObject<T>(Encoding.UTF8.GetString(buff));
        }
    }
}
