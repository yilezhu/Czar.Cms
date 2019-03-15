////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：定时任务                                                    
*│　作    者：yilezhu                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2019-03-13 11:17:00                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Czar.Cms.IRepository                                   
*│　接口名称： ITaskInfoRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Czar.Cms.Core.Repository;
using Czar.Cms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czar.Cms.IRepository
{
    public interface ITaskInfoRepository : IBaseRepository<TaskInfo, Int32>
    {
	     /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        /// <summary>
        /// 应用程序停止时暂停所有任务
        /// </summary>
        /// <returns></returns>
        Task<bool> SystemStoppedAsync();

        /// <summary>
        /// 应用程序启动时启动所有任务
        /// </summary>
        /// <returns></returns>
        Task<bool> ResumeSystemStoppedAsync();

        /// <summary>
        /// 根据ids更新状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> UpdateStatusByIdsAsync(Int32[] ids,int Status);

        /// <summary>
        /// 根据状态获取任务列表
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        Task<List<TaskInfo>> GetListByJobStatuAsync(int Status);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="Name">别名</param>
        /// <returns></returns>
        Task<Boolean> IsExistsNameAsync(string Name);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="Name">别名</param>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<Boolean> IsExistsNameAsync(string Name, Int32 Id);
    }
}