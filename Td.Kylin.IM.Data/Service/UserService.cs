using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;

namespace Td.Kylin.IM.Data.Service
{
    /// <summary>
    /// 用户数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class UserService<DbContext> : IUserService where DbContext : DataContext, new()
    {
        public Task<int> UpdateUserUnReceivedCount(long userID, int count)
        {
            using (var db = new DbContext())
            {
                var item = db.User.SingleOrDefault(p => p.UserID == userID);

                db.Entry(item).Property(p => p.UnReceivedCount).IsModified = true;

                item.UnReceivedCount += count;

                return db.SaveChangesAsync();
            }
        }
    }
}
