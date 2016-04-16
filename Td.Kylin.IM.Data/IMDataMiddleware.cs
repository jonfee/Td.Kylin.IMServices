using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;
using Td.Kylin.IM.Data.Enum;

namespace Td.Kylin.IM.Data
{
    /// <summary>
    /// IM数据中间件
    /// </summary>
    internal sealed class IMDataMiddleware
    {
        /// <summary>
        /// Http Request
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 数据库类型
        /// </summary>
        private readonly SqlProviderType _sqlProviderType;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly string _sqlconnectionString;

        #region RequestDelegate
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="next"></param>
        /// <param name="redisOptions">Redis Connections</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        public IMDataMiddleware(RequestDelegate next, SqlProviderType sqlType, string sqlConnection)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            
            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ArgumentNullException(nameof(sqlConnection));
            }
            _sqlProviderType = sqlType;
            _sqlconnectionString = sqlConnection;

            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            StartupConfig.SqlType = _sqlProviderType;

            StartupConfig.DbConnectionString = _sqlconnectionString;

            return _next(context);
        }

        #endregion

        #region 非Web程序中使用

        /// <summary>
        /// 实例化（非Web程序中使用）
        /// </summary>
        /// <param name="redisOptions"></param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="sqlConnection">数据库连接字符串</param>
        public IMDataMiddleware(SqlProviderType sqlType, string sqlConnection)
        {
            if (string.IsNullOrWhiteSpace(sqlConnection))
            {
                throw new ArgumentNullException(nameof(sqlConnection));
            }
            _sqlProviderType = sqlType;
            _sqlconnectionString = sqlConnection;
        }
        
        public void Invoke()
        {
            StartupConfig.SqlType = _sqlProviderType;

            StartupConfig.DbConnectionString = _sqlconnectionString;
        }

        #endregion
    }
}
