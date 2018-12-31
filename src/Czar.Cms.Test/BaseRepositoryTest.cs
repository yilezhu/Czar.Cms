/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：数据库基类操作测试                                                    
*│　作    者：yilezhu                                             
*│　版    本：1.0                                                 
*│　创建时间：2018/12/22 22:13:04                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.Test                                   
*│　类    名： BaseRepositoryTest                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Core.DbHelper;
using Czar.Cms.Core.Models;
using Czar.Cms.Core.Options;
using Czar.Cms.Core.Repository;
using Czar.Cms.IRepository;
using Czar.Cms.Models;
using Czar.Cms.Repository.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Czar.Cms.Test
{
    public class BaseRepositoryTest
    {

        [Fact]
        public void TestBaseFactory()
        {
            IServiceProvider serviceProvider = Common.BuildServiceForSqlServer();
            IArticleCategoryRepository categoryRepository = serviceProvider.GetService<IArticleCategoryRepository>();
            var category1 = new ArticleCategory
            {
                Title = "随笔1",
                ParentId = 0,
                ClassList = "",
                ClassLayer = 0,
                Sort = 0,
                ImageUrl = "",
                SeoTitle = "随笔的SEOTitle",
                SeoKeywords = "随笔的SeoKeywords",
                SeoDescription = "随笔的SeoDescription",
                IsDeleted = false,
            };
            var category2 = new ArticleCategory
            {
                Title = null,
                ParentId = 0,
                ClassList = "",
                ClassLayer = 0,
                Sort = 0,
                ImageUrl = "",
                SeoTitle = "随笔的SEOTitle",
                SeoKeywords = "随笔的SeoKeywords",
                SeoDescription = "随笔的SeoDescription",
                IsDeleted = false,
            };
            var categoryId = categoryRepository.Insert(category1);
            var categoryId2 = categoryRepository.Insert(category2);
            var list = categoryRepository.GetList();
            Assert.True(2 == list.Count());
            Assert.Equal("随笔1", list.FirstOrDefault().Title);
            categoryRepository.DeleteList("where 1=1");
            var count = categoryRepository.RecordCount();
            Assert.True(0 == count);
        }


        [Fact]
        public void TestUnitOfWorkNoException()
        {
            IServiceProvider serviceProvider = Common.BuildServiceForSqlServer();
            var category1 = new ArticleCategory
            {
                Title = "UnitOfWork分类1",
                ParentId = 0,
                ClassList = "",
                ClassLayer = 0,
                Sort = 0,
                ImageUrl = "",
                SeoTitle = "随笔的SEOTitle",
                SeoKeywords = "随笔的SeoKeywords",
                SeoDescription = "随笔的SeoDescription",
                IsDeleted = false,
            };
            var category2 = new ArticleCategory
            {
                Title = "UnitOfWork分类2",
                ParentId = 0,
                ClassList = "",
                ClassLayer = 0,
                Sort = 0,
                ImageUrl = "",
                SeoTitle = "随笔的SEOTitle",
                SeoKeywords = "随笔的SeoKeywords",
                SeoDescription = "随笔的SeoDescription",
                IsDeleted = false,
            };
            var menu = new Menu
            {
                ParentId = 0,
                Name = "测试1",
                AddManagerId = 1,
                IsDelete = false,
                IsSystem = false,
                IsDisplay = true,
                AddTime = DateTime.Now

                
            };
            IArticleCategoryRepository categoryRepository = serviceProvider.GetService<IArticleCategoryRepository>();

            var unitwork = serviceProvider.GetService<IUnitOfWork>();
            //var categoryId = categoryRepository.Insert(category1);
            var  count = 0;
            try
            {
                unitwork.Add(category1);
                unitwork.Add(category2);
                unitwork.Add(menu);
                count=unitwork.Commit();
            }
            catch
            {
                count = 0;
            }
            
            Assert.True(3 == count);
            #region categoryRepository
            var list = categoryRepository.GetList();
            Assert.True(2 == list.Count());
            var count2 = categoryRepository.DeleteList("where 1=1");
            Assert.True(2 == count2);
            var list2 = categoryRepository.GetList();
            Assert.True(0 == list2.Count());
            #endregion

        }

        [Fact]
        public void TestUnitOfWorkWithException()
        {
            IServiceProvider serviceProvider = Common.BuildServiceForSqlServer();
            var category1 = new ArticleCategory
            {
                Title = "UnitOfWork分类1",
                ParentId = 0,
                ClassList = "",
                ClassLayer = 0,
                Sort = 0,
                ImageUrl = "",
                SeoTitle = "随笔的SEOTitle",
                SeoKeywords = "随笔的SeoKeywords",
                SeoDescription = "随笔的SeoDescription",
                IsDeleted = false,
            };
            var category2 = new ArticleCategory
            {
                Title = null,
                ParentId = 0,
                ClassList = "",
                ClassLayer = 0,
                Sort = 0,
                ImageUrl = "",
                SeoTitle = "随笔的SEOTitle",
                SeoKeywords = "随笔的SeoKeywords",
                SeoDescription = "随笔的SeoDescription",
                IsDeleted = false,
            };
            var menu = new Menu
            {
                ParentId = 0,
                Name = "测试1",
                AddManagerId = 1,
                IsDelete = false,
                IsSystem = false,
                IsDisplay = true,
                AddTime = DateTime.Now
            };
            IArticleCategoryRepository categoryRepository = serviceProvider.GetService<IArticleCategoryRepository>();

            var unitwork = serviceProvider.GetService<IUnitOfWork>();
            //var categoryId = categoryRepository.Insert(category1);
            var count = 0;
            try
            {
                unitwork.Add(category1);
                unitwork.Add(category2);//标题为null会抛出异常
                unitwork.Add(menu);
                count = unitwork.Commit();
            }
            catch
            {
                
            }
            Assert.True(0 == count);
            #region categoryRepository
            var list = categoryRepository.GetList();
            Assert.True(0 == list.Count());
            var count2 = categoryRepository.DeleteList("where 1=1");
            Assert.True(0 == count2);
            var list2 = categoryRepository.GetList();
            Assert.True(0 == list2.Count());
            #endregion

        }

    }
}
