using Czar.Cms.Core.Models;
using Czar.Cms.Core.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Czar.Cms.Core.Extensions;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace Czar.Cms.Core.CodeGenerator
{
    /// <summary>
    /// yilezhu
    /// 2018.12.12
    /// 代码生成器。参考自：Zxw.Framework.NetCore
    /// <remarks>
    /// 根据数据库表以及表对应的列生成对应的数据库实体
    /// </remarks>
    /// </summary>
    public class CodeGenerator
    {
        private readonly string Delimiter = "\\";//分隔符，默认为windows下的\\分隔符

        private static CodeGenerateOption _options;
        public CodeGenerator(IOptions<CodeGenerateOption> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            _options = options.Value;
            if (_options.ConnectionString.IsNullOrWhiteSpace())
                throw new ArgumentNullException("不指定数据库连接串就生成代码，你想上天吗？");
            if (_options.DbType.IsNullOrWhiteSpace())
                throw new ArgumentNullException("不指定数据库类型就生成代码，你想逆天吗？");
            if (_options.DbType != DatabaseType.SqlServer.ToString())
                throw new ArgumentNullException("这是我的错，目前只支持MSSQL数据库的代码生成！后续更新MySQL");

            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (_options.OutputPath.IsNullOrWhiteSpace())
                _options.OutputPath = path;
            var flag = path.IndexOf("/bin");
            if (flag > 0)
                Delimiter = "/";//如果可以取到值，修改分割符
        }

        /// <summary>
        /// 根据数据库连接字符串生成数据库表对应的Model层代码
        /// </summary>
        /// <param name="isCoveredExsited">是否覆盖已存在的同名文件</param>
        public void GenerateModelCodesFromDatabase(bool isCoveredExsited = true)
        {
            //TODO 从数据库获取表列表以及生成实体对象
            if (_options.DbType != DatabaseType.SqlServer.ToString())
                throw new ArgumentNullException("这是我的错，目前只支持MSSQL数据库的代码生成！后续更新MySQL");
            DatabaseType dbType = DatabaseType.SqlServer;
            string strGetAllTables = @"SELECT DISTINCT d.name as TableName, f.value as TableComment
FROM      sys.syscolumns AS a LEFT OUTER JOIN
                sys.systypes AS b ON a.xusertype = b.xusertype INNER JOIN
                sys.sysobjects AS d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT OUTER JOIN
                sys.syscomments AS e ON a.cdefault = e.id LEFT OUTER JOIN
                sys.extended_properties AS g ON a.id = g.major_id AND a.colid = g.minor_id LEFT OUTER JOIN
                sys.extended_properties AS f ON d.id = f.major_id AND f.minor_id = 0";
            List<DbTable> tables = null;
            using (var conn = new SqlConnection(_options.ConnectionString))
            {
                tables = conn.Query<DbTable>(strGetAllTables).ToList();
                tables.ForEach(item =>
                {
                    string strGetTableColumns = @"SELECT   a.name AS ColName, CONVERT(bit, (CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') 
                = 1 THEN 1 ELSE 0 END)) AS IsIdentity, CONVERT(bit, (CASE WHEN
                    (SELECT   COUNT(*)
                     FROM      sysobjects
                     WHERE   (name IN
                                         (SELECT   name
                                          FROM      sysindexes
                                          WHERE   (id = a.id) AND (indid IN
                                                              (SELECT   indid
                                                               FROM      sysindexkeys
                                                               WHERE   (id = a.id) AND (colid IN
                                                                                   (SELECT   colid
                                                                                    FROM      syscolumns
                                                                                    WHERE   (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK')) 
                > 0 THEN 1 ELSE 0 END)) AS IsPrimaryKey, b.name AS ColumnType, COLUMNPROPERTY(a.id, a.name, 'PRECISION') 
                AS ColumnLength, CONVERT(bit, (CASE WHEN a.isnullable = 1 THEN 1 ELSE 0 END)) AS IsNullable, ISNULL(e.text, '') 
                AS DefaultValue, ISNULL(g.value, ' ') AS Comment
FROM      sys.syscolumns AS a LEFT OUTER JOIN
                sys.systypes AS b ON a.xtype = b.xusertype INNER JOIN
                sys.sysobjects AS d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT OUTER JOIN
                sys.syscomments AS e ON a.cdefault = e.id LEFT OUTER JOIN
                sys.extended_properties AS g ON a.id = g.major_id AND a.colid = g.minor_id LEFT OUTER JOIN
                sys.extended_properties AS f ON d.id = f.class AND f.minor_id = 0
WHERE   (b.name IS NOT NULL) AND (d.name = @TableName)
ORDER BY a.id, a.colorder";
                    item.Columns = conn.Query<DbTableColumn>(strGetTableColumns, new
                    {
                        TableName = item.TableName
                    }).ToList();

                    item.Columns.ForEach(x =>
                    {
                        var csharpType = DbColumnTypeCollection.DbColumnDataTypes.FirstOrDefault(t =>
                            t.DatabaseType == dbType && t.ColumnTypes.Split(',').Any(p =>
                                p.Trim().Equals(x.ColumnType, StringComparison.OrdinalIgnoreCase)))?.CSharpType;
                        if (string.IsNullOrEmpty(csharpType))
                        {
                            throw new SqlTypeException($"未从字典中找到\"{x.ColumnType}\"对应的C#数据类型，请更新DbColumnTypeCollection类型映射字典。");
                        }

                        x.CSharpType = csharpType;
                    });
                });
            }

            if (tables != null && tables.Any())
            {
                foreach (var table in tables)
                {
                    if (table.Columns.Any(c => c.IsPrimaryKey))
                    {
                        GenerateEntity(table, isCoveredExsited);
                    }
                }
            }
        }


        private void GenerateEntity(DbTable table, bool isCoveredExsited = true)
        {
            var modelPath = _options.OutputPath;
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }

            var fullPath = modelPath + Delimiter + table.TableName + ".cs";
            if (File.Exists(fullPath) && !isCoveredExsited)
                return;

            var pkTypeName = table.Columns.First(m => m.IsPrimaryKey).CSharpType;
            var sb = new StringBuilder();
            foreach (var column in table.Columns)
            {
                var tmp = GenerateEntityProperty(column);
                sb.AppendLine(tmp);
            }
            var content = ReadTemplate("ModelTemplate.txt");
            content = content.Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{ModelsNamespace}", _options.ModelsNamespace)
                .Replace("{Author}", _options.Author)
                .Replace("{Comment}", table.TableComment)
                .Replace("{ModelName}", table.TableName)
                .Replace("{ModelProperties}", sb.ToString());
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成属性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列</param>
        /// <returns></returns>
        private static string GenerateEntityProperty(DbTableColumn column)
        {
            var sb = new StringBuilder();
            if (!column.Comment.IsNullOrWhiteSpace())
            {
                sb.AppendLine("\t\t/// <summary>");
                sb.AppendLine("\t\t/// " + column.Comment);
                sb.AppendLine("\t\t/// </summary>");
            }
            var colType = column.CSharpType;
            if (colType.ToLower() != "string" && colType.ToLower() != "byte[]" && colType.ToLower() != "object" &&
                column.IsNullable)
            {
                colType = colType + "?";
            }
            sb.AppendLine($"\t\tpublic {colType} {column.ColName} " + "{get;set;}");
            return sb.ToString();
        }

        /// <summary>
        /// 从代码模板中读取内容
        /// </summary>
        /// <param name="templateName">模板名称，应包括文件扩展名称。比如：template.txt</param>
        /// <returns></returns>
        private string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var content = string.Empty;
            using (var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.CodeTemplate.{templateName}"))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        private static void WriteAndSave(string fileName, string content)
        {
            //实例化一个文件流--->与写入文件相关联
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }

    }
}
