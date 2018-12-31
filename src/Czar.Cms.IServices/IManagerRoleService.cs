/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理员角色                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-31 16:43:28                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.IServices                                   
*│　接口名称： IManagerRoleRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.IServices
{
    public interface IManagerRoleService
    {
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns>table数据</returns>
        TableDataModel LoadData(ManagerRoleRequestModel model);

        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        BaseResult AddOrModify(ManagerRoleAddOrModifyModel item);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleId">角色主键id</param>
        /// <returns>结果实体</returns>
        BaseResult DeleteIds(int[] roleId);
    }
}