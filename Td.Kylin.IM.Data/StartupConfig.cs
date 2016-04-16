
using Td.Kylin.IM.Data.Enum;

namespace Td.Kylin.IM.Data
{
    /// <summary>
    /// DB 配置类
    /// </summary>
    public sealed class StartupConfig
    {
        /// <summary>
        /// SQL类型
        /// </summary>
        public static SqlProviderType SqlType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string DbConnectionString { get; set; }
    }
}
