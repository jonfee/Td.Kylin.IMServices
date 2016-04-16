using System.Configuration;
using Td.Kylin.IM.Data;
using Td.Kylin.IM.Data.Enum;

namespace IMService
{
    /// <summary>
    /// 用作启动项类
    /// </summary>
    internal class Startup
    {
        static Startup()
        {
            string conn = ConfigurationManager.ConnectionStrings["KylinIMConnectionString"].ConnectionString;

            IMDataInjection.UseIMData(SqlProviderType.POSTGRESQL, conn);
        }
    }
}
