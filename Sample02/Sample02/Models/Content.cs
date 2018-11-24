using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample01.Models
{
    /// <summary>
    /// 2018.11.19
    /// 祝雷
    /// 内容实体
    /// </summary>
    public class Content
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 状态 1正常 0删除
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime add_time { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime modify_time { get; set; }
    }
}
