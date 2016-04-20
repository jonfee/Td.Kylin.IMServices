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
    /// 用户关系数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class UserRelationService<DbContext> : IUserRelationService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 两个用户建立关联（系）
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        /// <returns></returns>
        public async Task<int> CreateRelation(long firstUser, long secondUser)
        {
            using (var db = new DbContext())
            {
                var item = new UserRelation
                {
                    CreateTime = DateTime.Now,
                    FirstDelete = false,
                    FirstGroupName = string.Empty,
                    FirstUser = firstUser,
                    SecondDelete = false,
                    SecondGroupName = string.Empty,
                    SecondUser = secondUser
                };

                db.UserRelation.Add(item);

                return await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 检测两个用户之间是否存在关联（系）
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        /// <returns></returns>
        public bool HasRelation(long firstUser, long secondUser)
        {
            using (var db = new DbContext())
            {
                var query = from p in db.UserRelation
                            where (p.FirstUser == firstUser && p.SecondUser == secondUser) || (p.FirstUser == secondUser && p.SecondUser == firstUser)
                            select p;

                return query.Count() > 0;
            }
        }
    }
}
