using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;

namespace Td.Kylin.IM.Data.Service
{
    /// <summary>
    /// 未发送的消息数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class UnsendMessageService<DbContext> : IUnsendMessageService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 添加消息到未发送记录
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<int> AddMessage(UnSendMessage message)
        {
            using (var db = new DbContext())
            {
                db.UnSendMessage.Add(message);

                return await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="msgIDs"></param>
        /// <returns></returns>
        public async Task<int> DeleteMessage(long[] msgIDs)
        {
            using (var db = new DbContext())
            {
                List<UnSendMessage> entities = new List<UnSendMessage>();

                Array.ForEach(msgIDs, (id) =>
                {
                    entities.Add(new UnSendMessage { MessageID = id });
                });

                db.UnSendMessage.AttachRange(entities);

                db.UnSendMessage.RemoveRange(entities);

                return await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 获取用户未接收到的消息集合
        /// </summary>
        /// <param name="receiverID"></param>
        /// <returns></returns>
        public List<UnSendMessage> GetList(long receiverID)
        {
            using (var db = new DbContext())
            {
                var query = from p in db.UnSendMessage
                            where p.ReceiverID == receiverID
                            orderby p.SendTime ascending
                            select p;

                return query.ToList();
            }
        }
    }
}
