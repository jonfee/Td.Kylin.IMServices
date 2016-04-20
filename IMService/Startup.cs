using System.Configuration;
using System.Net;
using Td.Kylin.IM.Data;
using Td.Kylin.IM.Data.Enum;

namespace IMService
{
    /// <summary>
    /// 用作启动项类
    /// </summary>
    internal sealed class Startup
    {
       public static void  Init()
        {
            string conn = ConfigurationManager.ConnectionStrings["KylinIMConnectionString"].ConnectionString;

            IMDataInjection.UseIMData(SqlProviderType.POSTGRESQL, conn);

            ServerConfig = new ServerConfig
            {
                ServerIP = IPAddress.Parse(ConfigurationManager.AppSettings["Host"]),
                Port = int.Parse(ConfigurationManager.AppSettings["Port"]),
                MaxConnectionCount = int.Parse(ConfigurationManager.AppSettings["MaxConnectionCount"])
            };
        }

        /// <summary>
        /// IM服务端配置
        /// </summary>
        public static ServerConfig ServerConfig;
    }
}
