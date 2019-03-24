/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：委托帮助类                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/3/24 10:39:19                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Core.Helper                                   
*│　类    名： DelegateHelper                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Czar.Cms.Core.Helper
{
    public class DelegateHelper
    {
        private readonly ILogger<DelegateHelper> Logger;
        public DelegateHelper(ILogger<DelegateHelper> logger = null)
        {
            Logger = logger;
        }
       
        public void TryExecute(Action action, string funcName = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{funcName}() Blow Up.");
            }
        }

        public T TryExecute<T>(Func<T> func, string funcName = null)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Logger.LogError( ex, $"{funcName}() Blow Up.");
                return default(T);
            }
        }

        public async Task<T> TryExecuteAsync<T>(Func<Task<T>> func, string funcName = null)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{funcName}() Blow Up.");
                return default(T);
            }
        }

    }
}
