using System.Threading.Tasks;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 用户数据服务接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 更新用户未接收消息的数量
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="count"></param>
        /// <returns>受影响的行数，为1时表示操作成功，否则失败</returns>
        Task<int> UpdateUserUnReceivedCount(long userID, int count);
    }
}
