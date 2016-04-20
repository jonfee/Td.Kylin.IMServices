using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;
using Td.Kylin.IM.Data.Model;

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
                                ID = p.UserID,
                                Name = p.NickName
                            };

                return query.ToDictionary(k => k.ID, v => v.Name);
            }
        }

        /// <summary>
        /// 更新用户最后登录信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="name"></param>
        /// <param name="userType"></param>
        /// <param name="lastLoginAddress"></param>
        /// <param name="lastLoginTime"></param>
        /// <returns></returns>
        public async Task<int> UpdateLastInfo(long userID, string name, int userType, string lastLoginAddress, DateTime lastLoginTime)
        {
            using (var db = new DbContext())
            {
                var item = db.User.SingleOrDefault(p => p.UserID == userID);

                if (null == item)
                {
                    item = new User
                    {
                        CreateTime = DateTime.Now,
                        LastLoginAddress = lastLoginAddress,
                        LastLoginTime = lastLoginTime,
                        NickName = name,
                        Photo = string.Empty,
                        PrevLoginAddress = lastLoginAddress,
                        PrevLoginTime = lastLoginTime,
                        Status = 0,
                        UserID = userID,
                        UserType = userType
                    };
                    db.User.Add(item);
                }
                else
                {
                    db.Entry(item).Property(p => p.NickName).IsModified = true;
                    db.Entry(item).Property(p => p.UserType).IsModified = true;
                    db.Entry(item).Property(p => p.PrevLoginAddress).IsModified = true;
                    db.Entry(item).Property(p => p.PrevLoginTime).IsModified = true;
                    db.Entry(item).Property(p => p.LastLoginAddress).IsModified = true;
                    db.Entry(item).Property(p => p.LastLoginTime).IsModified = true;

                    item.NickName = name;
                    item.UserType = userType;
                    item.PrevLoginAddress = item.LastLoginAddress;
                    item.PrevLoginTime = item.PrevLoginTime;
                    item.LastLoginAddress = lastLoginAddress;
                    item.LastLoginTime = lastLoginTime;
                }

                return await db.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserLoginInfo GetUserLoginInfo(long userID)
        {
            using (var db = new DbContext())
            {
                var query = from p in db.User
                            where p.UserID == userID
                            select new UserLoginInfo
                            {
                                UserID = p.UserID,
                                LastLoginAddress = p.LastLoginAddress,
                                LastLoginTime = p.LastLoginTime,
                                PrevLoginAddress = p.PrevLoginAddress,
                                PrevLoginTime = p.PrevLoginTime
                            };

                return query.FirstOrDefault();
            }
        }
    }
}
