using System;
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
            SqlType = new Func<SqlProviderType>(() =>
              {
                  string sqlType = ConfigurationManager.AppSettings["SqlType"]??string.Empty;

                  switch (sqlType.ToLower())
                  {
                      case "pgsql":
                          return SqlProviderType.POSTGRESQL;
                      case "mssql":
                      default:
                          return SqlProviderType.MSSQL;
                  }
              }).Invoke();

            string conn = ConfigurationManager.ConnectionStrings["KylinIMConnectionString"].ConnectionString;

            IMDataInjection.UseIMData(SqlType, conn);

            ServerConfig = new ServerConfig
            {
                ServerIP = IPAddress.Parse(ConfigurationManager.AppSettings["Host"]),
                Port = int.Parse(ConfigurationManager.AppSettings["Port"]),
                MaxConnectionCount = int.Parse(ConfigurationManager.AppSettings["MaxConnectionCount"])
            };
        }

        public static SqlProviderType SqlType;

        /// <summary>
        /// IM服务端配置
        /// </summary>
        public static ServerConfig ServerConfig;
    }
}
