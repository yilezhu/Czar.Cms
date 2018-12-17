/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：文章接口                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/17 12:40:54                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.IRepository                                   
*│　接口名称： IArticleRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Core.Repository;
using Czar.Cms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.IRepository
{
    public interface IArticleRepository : IBaseRepository<Article, int>
    {
    }
}
