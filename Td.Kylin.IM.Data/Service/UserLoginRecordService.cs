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
    internal sealed class UserLoginRecordService<DbContext> : IUserLoginRecordService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 记录登录信息
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<int> AddRecord(UserLoginRecords record)
        {
            using (var db = new DbContext())
            {
                db.UserLoginRecords.Add(record);

                return await db.SaveChangesAsync();
            }
        }
    }
}
