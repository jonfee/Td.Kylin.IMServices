using System;
using System.Threading.Tasks;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;

namespace Td.Kylin.IM.Data.Service
{
    /// <summary>
    /// 错误日志数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class ErrorLogService<DbContext> : IErrorLogService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="tag"></param>
        public Task<int> AddLog(Exception ex, string tag)
        {
            if (null == ex) return null;

            using (var db = new DbContext())
            {
                ErrorLog log = new ErrorLog
                {
                    Tag = tag,
                    CreateTime = DateTime.Now,
                    HelpLink = ex.HelpLink,
                    LogID = Guid.NewGuid().GetHashCode(),
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                };

                db.ErrorLog.Add(log);

                return db.SaveChangesAsync();
            }
        }
    }
}
