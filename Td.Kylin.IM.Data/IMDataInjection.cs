using Microsoft.AspNet.Builder;
using System;
using Td.Kylin.IM.Data.Enum;

namespace Td.Kylin.IM.Data
{
    /// <summary>
    /// Injection IM For DataContext
    /// </summary>
    public static class IMDataInjection
    {
        /// <summary>
        /// 注入IM Data服务
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <returns></returns>
        public static IApplicationBuilder UseIMData(this IApplicationBuilder builder, SqlProviderType sqlType, string sqlConnection)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.Use(next => new IMDataMiddleware(next, sqlType, sqlConnection).Invoke);
        }

        /// <summary>
        /// 注入IM Data服务（非Web形式注入）
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        /// <returns></returns>
        public static void UseIMData(SqlProviderType sqlType, string sqlConnection)
        {
            new IMDataMiddleware(sqlType, sqlConnection).Invoke();
        }
    }
}
