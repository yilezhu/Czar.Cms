/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/1 21:31:10                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Core.Helper                                   
*│　类    名： CacheHelper                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.Core.Helper
{
    public class CacheHelper
    {
        static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        /// <summary>
        /// 创建缓存项的文件
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        public static void Set(string key, object value)
        {
            if (key != null)
            {
                cache.Set(key, value);
            }
        }
        /// <summary>
        /// 创建缓存项过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="expires">过期时间(秒)</param>
        public static void Set(string key, object value, int expires)
        {
            if (key != null)
            {
                cache.Set(key, value, new MemoryCacheEntryOptions()
                    //设置缓存时间，如果被访问重置缓存时间。设置相对过期时间x秒
                    .SetSlidingExpiration(TimeSpan.FromSeconds(expires)));
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>object对象</returns>
        public static object Get(string key)
        {
            object val = null;
            if (key != null && cache.TryGetValue(key, out val))
            {

                return val;
            }
            else
            {
                return default(object);
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">T对象</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }


        /// <summary>
        /// 移除缓存项的文件
        /// </summary>
        /// <param name="key">缓存Key</param>
        public static void Remove(string key)
        {
            cache.Remove(key);
        }

    }
}
