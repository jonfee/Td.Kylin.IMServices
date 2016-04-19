using System.Threading.Tasks;
using Td.Kylin.IM.Data.Entity;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 用户登录记录数据服务接口
    /// </summary>
    public interface IUserLoginRecordService
    {
        /// <summary>
        /// 记录登录信息
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<int> AddRecord(UserLoginRecords record);
    }
}
