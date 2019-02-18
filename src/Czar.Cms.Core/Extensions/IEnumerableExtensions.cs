/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：枚举类型的扩展                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2019/1/5 12:26:54                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Core.Extensions                                   
*│　类    名： IEnumerableExtensions                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Czar.Cms.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 列表生成树形节点
        /// </summary>
        /// <typeparam name="T">集合对象的类型</typeparam>
        /// <typeparam name="K">父节点的类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="idSelector">主键ID</param>
        /// <param name="parentIdSelector">父节点</param>
        /// <param name="rootId">根节点</param>
        /// <returns>列表生成树形节点</returns>
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
            this IEnumerable<T> collection,
            Func<T, K> idSelector,
            Func<T, K> parentIdSelector,
            K rootId = default(K))
        {
            foreach (var c in collection.Where(u =>
            {
                var selector = parentIdSelector(u);
                return (rootId == null && selector == null)
                || (rootId != null && rootId.Equals(selector));
            }))
            {
                yield return new TreeItem<T>
                {
                    Item = c,
                    Children = collection.GenerateTree(idSelector, parentIdSelector, idSelector(c))
                };
            }
        }
        /// <summary>
        /// 把数组转为逗号连接的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ArrayToString(this IEnumerable collection)
        {
            string resStr = "";
            foreach (var item in collection)
            {
                if (resStr != "")
                {
                    resStr += ",";
                }
                resStr += item;
            }
            return resStr;
        }

        /// <summary>
        /// 把数组转为split分割后连接的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string ArrayToString(this IEnumerable collection,string split)
        {
            string resStr = "";
            if (split.IsNullOrEmpty())
            {
                split = ",";
            }
            foreach (var item in collection)
            {
                if (resStr != "")
                {
                    resStr += split;
                }
                resStr += item;
            }
            return resStr;
        }
    }
}
