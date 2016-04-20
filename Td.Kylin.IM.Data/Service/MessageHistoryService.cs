using System.Threading.Tasks;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;

namespace Td.Kylin.IM.Data.Service
{
    /// <summary>
    /// 消息历史数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class MessageHistoryService<DbContext> : IMessageHistoryService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 添加消息到历史
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<int> AddMessage(MessageHistory message)
        {
            using (var db = new DbContext())
            {
                db.MessageHistory.Add(message);

                return await db.SaveChangesAsync();
            }
        }
    }
}
