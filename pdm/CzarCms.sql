/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2018/12/11 23:02:45                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Article') and o.name = 'FK_ARTICLE_RELATIONS_ARTICLEC')
alter table Article
   drop constraint FK_ARTICLE_RELATIONS_ARTICLEC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Manager') and o.name = 'FK_MANAGER_RELATIONS_MANAGERR')
alter table Manager
   drop constraint FK_MANAGER_RELATIONS_MANAGERR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ManagerLog') and o.name = 'FK_MANAGERL_RELATIONS_MANAGER')
alter table ManagerLog
   drop constraint FK_MANAGERL_RELATIONS_MANAGER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolePermission') and o.name = 'FK_ROLEPERM_RELATIONS_MENU')
alter table RolePermission
   drop constraint FK_ROLEPERM_RELATIONS_MENU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolePermission') and o.name = 'FK_ROLEPERM_RELATIONS_MANAGERR')
alter table RolePermission
   drop constraint FK_ROLEPERM_RELATIONS_MANAGERR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Article')
            and   type = 'U')
   drop table Article
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ArticleCategory')
            and   type = 'U')
   drop table ArticleCategory
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Manager')
            and   name  = 'Relationship_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index Manager.Relationship_3_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Manager')
            and   type = 'U')
   drop table Manager
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ManagerLog')
            and   type = 'U')
   drop table ManagerLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ManagerRole')
            and   type = 'U')
   drop table ManagerRole
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Menu')
            and   type = 'U')
   drop table Menu
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RolePermission')
            and   name  = 'Relationship_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index RolePermission.Relationship_2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RolePermission')
            and   name  = 'Relationship_1_FK'
            and   indid > 0
            and   indid < 255)
   drop index RolePermission.Relationship_1_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RolePermission')
            and   type = 'U')
   drop table RolePermission
go

/*==============================================================*/
/* Table: Article                                               */
/*==============================================================*/
create table Article (
   Id                   int                  identity,
   CategoryId           int                  not null,
   Title                varchar(128)         not null,
   ImageUrl             varchar(128)         null,
   Content              text                 null,
   ViewCount            int                  not null,
   Sort                 int                  not null,
   Author               varchar(64)          null,
   Source               varchar(128)         null,
   SeoTitle             varchar(128)         null,
   SeoKeyword           varchar(256)         null,
   SeoDescription       varchar(512)         null,
   AddManagerId         int                  not null,
   AddTime              datetime             not null default getdate(),
   ModifyManagerId      int                  null,
   ModifyTime           datetime             null,
   IsTop                bit                  not null default 0,
   IsSlide              bit                  not null default 0,
   IsRed                bit                  not null default 0,
   IsPublish            bit                  not null default 0,
   IsDeleted            bit                  not null default 0,
   constraint PK_ARTICLE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Article') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Article' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '文章', 
   'user', @CurrentUser, 'table', 'Article'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Article', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CategoryId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'CategoryId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类ID',
   'user', @CurrentUser, 'table', 'Article', 'column', 'CategoryId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Title')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'Title'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文章标题',
   'user', @CurrentUser, 'table', 'Article', 'column', 'Title'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ImageUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'ImageUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片地址',
   'user', @CurrentUser, 'table', 'Article', 'column', 'ImageUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Content')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'Content'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文章内容',
   'user', @CurrentUser, 'table', 'Article', 'column', 'Content'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ViewCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'ViewCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '浏览次数',
   'user', @CurrentUser, 'table', 'Article', 'column', 'ViewCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Sort')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'Sort'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序',
   'user', @CurrentUser, 'table', 'Article', 'column', 'Sort'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Author')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'Author'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '作者',
   'user', @CurrentUser, 'table', 'Article', 'column', 'Author'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Source')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'Source'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '来源',
   'user', @CurrentUser, 'table', 'Article', 'column', 'Source'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SeoTitle')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'SeoTitle'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SEO标题',
   'user', @CurrentUser, 'table', 'Article', 'column', 'SeoTitle'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SeoKeyword')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'SeoKeyword'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SEO关键字',
   'user', @CurrentUser, 'table', 'Article', 'column', 'SeoKeyword'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SeoDescription')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'SeoDescription'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SEO描述',
   'user', @CurrentUser, 'table', 'Article', 'column', 'SeoDescription'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'AddManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加人ID',
   'user', @CurrentUser, 'table', 'Article', 'column', 'AddManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'AddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'Article', 'column', 'AddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'ModifyManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人ID',
   'user', @CurrentUser, 'table', 'Article', 'column', 'ModifyManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'ModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'Article', 'column', 'ModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsTop')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsTop'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否置顶',
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsTop'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSlide')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsSlide'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否轮播显示',
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsSlide'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsRed')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsRed'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否热门',
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsRed'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsPublish')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsPublish'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否发布',
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsPublish'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Article')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'Article', 'column', 'IsDeleted'
go

/*==============================================================*/
/* Table: ArticleCategory                                       */
/*==============================================================*/
create table ArticleCategory (
   Id                   int                  identity,
   Title                varchar(128)         not null,
   ParentId             int                  not null,
   ClassList            varchar(128)         null,
   ClassLayer           int                  null,
   Sort                 int                  not null,
   ImageUrl             varchar(128)         null,
   SeoTitle             varchar(128)         null,
   SeoKeywords          varchar(256)         null,
   SeoDescription       varchar(512)         null,
   IsDeleted            bit                  not null default 0,
   constraint PK_ARTICLECATEGORY primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ArticleCategory') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ArticleCategory' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '文章分类', 
   'user', @CurrentUser, 'table', 'ArticleCategory'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Title')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'Title'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类标题',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'Title'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParentId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ParentId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父分类ID',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ParentId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClassList')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ClassList'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类别ID列表(逗号分隔开)',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ClassList'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClassLayer')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ClassLayer'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类别深度',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ClassLayer'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Sort')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'Sort'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'Sort'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ImageUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ImageUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类图标',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'ImageUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SeoTitle')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'SeoTitle'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类SEO标题',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'SeoTitle'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SeoKeywords')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'SeoKeywords'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类SEO关键字',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'SeoKeywords'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SeoDescription')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'SeoDescription'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类SEO描述',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'SeoDescription'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ArticleCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'IsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'ArticleCategory', 'column', 'IsDeleted'
go

/*==============================================================*/
/* Table: Manager                                               */
/*==============================================================*/
create table Manager (
   Id                   int                  identity,
   RoleId               int                  not null,
   UserName             varchar(32)          not null,
   Password             varchar(128)         not null,
   Avatar               varchar(256)         null,
   NickName             varchar(32)          null,
   Mobile               varchar(16)          null,
   Email                varchar(128)         null,
   LoginCount           int                  null,
   LoginLastIp          varchar(64)          null,
   LoginLastTime        datetime             null,
   AddManagerId         int                  not null,
   AddTime              datetime             not null default getdate(),
   ModifyManagerId      int                  null,
   ModifyTime           datetime             null,
   IsLock               bit                  not null default 0,
   IsDelete             bit                  not null default 0,
   Remark               varchar(128)         null,
   constraint PK_MANAGER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Manager') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Manager' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '后台管理员', 
   'user', @CurrentUser, 'table', 'Manager'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'RoleId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色ID',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'RoleId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'UserName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'UserName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Password')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Password'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Password'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Avatar')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Avatar'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头像',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Avatar'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NickName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'NickName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户昵称',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'NickName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Mobile')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Mobile'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '手机号码',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Mobile'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Email')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Email'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱地址',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Email'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LoginCount')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'LoginCount'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '登录次数',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'LoginCount'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LoginLastIp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'LoginLastIp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后一次登录IP',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'LoginLastIp'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LoginLastTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'LoginLastTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后一次登录时间',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'LoginLastTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'AddManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加人',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'AddManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'AddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'AddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'ModifyManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'ModifyManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'ModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'ModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsLock')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'IsLock'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否锁定',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'IsLock'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'IsDelete'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Manager')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Remark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Remark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Manager', 'column', 'Remark'
go

/*==============================================================*/
/* Index: Relationship_3_FK                                     */
/*==============================================================*/
create index Relationship_3_FK on Manager (
RoleId ASC
)
go

/*==============================================================*/
/* Table: ManagerLog                                            */
/*==============================================================*/
create table ManagerLog (
   Id                   int                  identity,
   ActionType           varchar(32)          null,
   AddManageId          int                  not null,
   AddManagerNickName   varchar(64)          null,
   AddTime              datetime             not null default getdate(),
   AddIp                varchar(64)          null,
   Remark               varchar(256)         null,
   constraint PK_MANAGERLOG primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ManagerLog') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ManagerLog' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '操作日志', 
   'user', @CurrentUser, 'table', 'ManagerLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'ActionType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作类型',
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'ActionType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddManageId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddManageId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddManageId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddManagerNickName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddManagerNickName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作人名称',
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddManagerNickName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作时间',
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddIp')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddIp'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作IP',
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'AddIp'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Remark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'Remark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ManagerLog', 'column', 'Remark'
go

/*==============================================================*/
/* Table: ManagerRole                                           */
/*==============================================================*/
create table ManagerRole (
   Id                   int                  identity,
   RoleName             varchar(64)          not null,
   RoleType             int                  not null,
   IsSystem             bit                  not null,
   AddManagerId         int                  not null,
   AddTime              datetime             not null default getdate(),
   ModifyManagerId      int                  null,
   ModifyTime           datetime             null,
   IsDelete             bit                  not null,
   Remark               varchar(128)         null,
   constraint PK_MANAGERROLE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ManagerRole') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ManagerRole' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '后台管理员角色', 
   'user', @CurrentUser, 'table', 'ManagerRole'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'RoleName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色名称',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'RoleName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'RoleType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色类型1超管2系管',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'RoleType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSystem')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'IsSystem'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否系统默认',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'IsSystem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'AddManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加人',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'AddManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'AddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'AddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'ModifyManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'ModifyManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'ModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'ModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'IsDelete'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ManagerRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Remark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'Remark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ManagerRole', 'column', 'Remark'
go

/*==============================================================*/
/* Table: Menu                                                  */
/*==============================================================*/
create table Menu (
   Id                   int                  identity,
   ParentId             int                  not null,
   Name                 varchar(32)          not null,
   DisplayName          varchar(128)         null,
   IconUrl              varchar(128)         null,
   LinkUrl              varchar(128)         null,
   Sort                 int                  null,
   Permission           varchar(256)         null,
   IsDisplay            bit                  not null,
   IsSystem             bit                  not null,
   AddManagerId         int                  not null,
   AddTime              datetime             not null default getdate(),
   ModifyManagerId      int                  null,
   ModifyTimes          datetime             null,
   IsDelete             bit                  not null,
   constraint PK_MENU primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Menu') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Menu' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '后台管理菜单', 
   'user', @CurrentUser, 'table', 'Menu'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ParentId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'ParentId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父菜单ID',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'ParentId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DisplayName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'DisplayName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '显示名称',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'DisplayName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IconUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IconUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图标地址',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IconUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LinkUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'LinkUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '链接地址',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'LinkUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Sort')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Sort'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序数字',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Sort'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Permission')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Permission'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作权限（按钮权限时使用）',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Permission'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDisplay')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IsDisplay'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否显示',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IsDisplay'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSystem')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IsSystem'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否系统默认',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IsSystem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'AddManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加人',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'AddManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AddTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'AddTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'AddTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyManagerId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'ModifyManagerId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'ModifyManagerId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModifyTimes')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'ModifyTimes'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'ModifyTimes'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDelete')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IsDelete'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'IsDelete'
go

/*==============================================================*/
/* Table: RolePermission                                        */
/*==============================================================*/
create table RolePermission (
   Id                   int                  identity,
   RoleId               int                  not null,
   MenuId               int                  not null,
   Permission           varchar(128)         null,
   constraint PK_ROLEPERMISSION primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('RolePermission') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'RolePermission' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '角色权限表', 
   'user', @CurrentUser, 'table', 'RolePermission'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolePermission')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolePermission')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RoleId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'RoleId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色主键',
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'RoleId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolePermission')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MenuId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'MenuId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单主键',
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'MenuId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('RolePermission')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Permission')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'Permission'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作类型（功能权限）',
   'user', @CurrentUser, 'table', 'RolePermission', 'column', 'Permission'
go

/*==============================================================*/
/* Index: Relationship_1_FK                                     */
/*==============================================================*/
create index Relationship_1_FK on RolePermission (
MenuId ASC
)
go

/*==============================================================*/
/* Index: Relationship_2_FK                                     */
/*==============================================================*/
create index Relationship_2_FK on RolePermission (
RoleId ASC
)
go

alter table Article
   add constraint FK_ARTICLE_RELATIONS_ARTICLEC foreign key (CategoryId)
      references ArticleCategory (Id)
go

alter table Manager
   add constraint FK_MANAGER_RELATIONS_MANAGERR foreign key (RoleId)
      references ManagerRole (Id)
go

alter table ManagerLog
   add constraint FK_MANAGERL_RELATIONS_MANAGER foreign key (AddManageId)
      references Manager (Id)
go

alter table RolePermission
   add constraint FK_ROLEPERM_RELATIONS_MENU foreign key (MenuId)
      references Menu (Id)
go

alter table RolePermission
   add constraint FK_ROLEPERM_RELATIONS_MANAGERR foreign key (RoleId)
      references ManagerRole (Id)
go

