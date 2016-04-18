using System.Collections.Generic;
using System.Linq;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;

namespace Td.Kylin.IM.Data.Service
{
    internal sealed class UnsendMessageService<DbContext> : IUnsendMessageService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 获取用户未接收到的消息集合
        /// </summary>
        /// <param name="receiverID"></param>
        /// <returns></returns>
        public List<UnSendMessage> GetList(long receiverID)
        {
            using (var db = new DbContext())
            {
                var query = from p in db.UnreadMessage
                            where p.ReceiverID == receiverID
                            orderby p.SendTime ascending
                            select p;

                return query.ToList();
            }
        }
    }
}
