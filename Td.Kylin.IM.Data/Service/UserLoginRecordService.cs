using System.Threading.Tasks;
using Td.Kylin.IM.Data.Context;
using Td.Kylin.IM.Data.Entity;
using Td.Kylin.IM.Data.IService;

namespace Td.Kylin.IM.Data.Service
{
    /// <summary>
    /// 用户登录记录数据服务
    /// </summary>
    /// <typeparam name="DbContext"></typeparam>
    internal sealed class UserLoginRecordService<DbContext> : IUserLoginRecordService where DbContext : DataContext, new()
    {
        /// <summary>
        /// 记录登录信息
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public Task<int> AddRecord(UserLoginRecords record)
        {
            using (var db = new DbContext())
            {
                db.UserLoginRecords.Add(record);

                return db.SaveChangesAsync();
            }
        }
    }
}
