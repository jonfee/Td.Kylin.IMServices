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
        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public Dictionary<long, string> GetUserName(long[] userIds)
        {
            using (var db = new DbContext())
            {
                var query = from p in db.User
                            where userIds.Contains(p.UserID)
                            select new
                            {
                                ID=p.UserID,
                                Name=p.NickName
                            };

                return query.ToDictionary(k => k.ID, v => v.Name);
            }
        }

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="lastLoginTime"></param>
        /// <returns></returns>
        public Task<int> UpdateLastLoginTime(long userID, DateTime lastLoginTime)
        {
            using (var db = new DbContext())
            {
                var item = db.User.SingleOrDefault(p => p.UserID == userID);

                db.Entry(item).Property(p => p.LastLoginTime).IsModified = true;

                item.LastLoginTime = lastLoginTime;

                return db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 更新用户未接收消息的数量
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
