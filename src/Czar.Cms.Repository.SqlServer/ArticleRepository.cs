/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章接口实现                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/17 12:44:29                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Repository.SqlServer                                   
*│　类    名： ArticleRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Core.DbHelper;
using Czar.Cms.Core.Options;
using Czar.Cms.Core.Repository;
using Czar.Cms.IRepository;
using Czar.Cms.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.Repository.SqlServer
{
    public class ArticleRepository:BaseRepository<Article,int>, IArticleRepository
    {
        public ArticleRepository(IConfiguration configuration)
        {
            _dbOpion = new DbOpion();
            configuration.Bind("DbOpion", _dbOpion);
            if (_dbOpion == null)
            {
                throw new ArgumentNullException(nameof(DbOpion));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOpion.DbType, _dbOpion.ConnectionString);
        }

    }
}
