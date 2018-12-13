using Czar.Cms.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Czar.Cms.Core.Extensions;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

namespace Czar.Cms.Core.DbHelper
{
    /// <summary>
    /// yilezhu
    /// 2018.12.13
    /// 数据库连接辅助类
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        private static DatabaseType GetDataBaseType(string dbtype)
        {
            if (dbtype.IsNullOrWhiteSpace())
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你想上天吗？");
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(string dbtype,string strConn)
        {
            IDbConnection connection = null;
            if (dbtype.IsNullOrWhiteSpace())
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你想上天吗？");
            if (strConn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("获取数据库连接居然不传数据库类型，你想上天吗？");          
            var dbType = GetDataBaseType(dbtype);           
          
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                case DatabaseType.MySQL:
                    connection = new MySqlConnection(strConn);
                    break;
                case DatabaseType.PostgreSQL:
                    connection = new NpgsqlConnection(strConn);
                    break;
                default:
                    throw new ArgumentNullException($"这是我的错，还不支持的{dbType.ToString()}数据库类型");
                  
            }

            return connection;
        }
    }
}
