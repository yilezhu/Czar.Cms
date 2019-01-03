/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：工作单元接口                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/22 19:40:05                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Core.Repository                                   
*│　接口名称： IUnitOfWork                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.Core.Repository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 注册新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 注册更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        void Update<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 注册删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 提交事务
        /// </summary>
        int Commit();
    }
}
