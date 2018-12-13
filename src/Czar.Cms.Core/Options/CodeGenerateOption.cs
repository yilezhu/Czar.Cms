using Czar.Cms.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.Core.Options
{
    /// <summary>
    /// yilezhu
    /// 2018.12.12
    /// 代码生成选项
    /// </summary>
    public class CodeGenerateOption
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType  { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 代码生成时间
        /// </summary>
        public string GeneratorTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// 实体命名空间
        /// </summary>
        public string ModelsNamespace { get; set; }
    }
}
