/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：后台管理菜单                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2018-12-31 16:43:28                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.IServices                                   
*│　接口名称： IMenuRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Models;
using Czar.Cms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Czar.Cms.IServices
{
    public interface IMenuService
    {
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns>table数据</returns>
        TableDataModel LoadData(MenuRequestModel model);

        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        Task<BaseResult> AddOrModifyAsync(MenuAddOrModifyModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键id数组</param>
        /// <returns>结果实体</returns>
        Task<BaseResult> DeleteIdsAsync(int[] Ids);

        /// <summary>
        /// 更改显示的状态
        /// </summary>
        /// <param name="item">改变状态实体</param>
        /// <returns></returns>
        Task<BaseResult> ChangeDisplayStatusAsync(ChangeStatusModel item);

        /// <summary>
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        Task<BooleanResult> IsExistsNameAsync(MenuAddOrModifyModel item);

        /// <summary>
        /// 根据父节点返回列表
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        List<Menu> GetChildListByParentId(int ParentId);
    }
}