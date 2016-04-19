using System.Threading.Tasks;
using Td.Kylin.IM.Data.Entity;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 历史消息数据服务接口
    /// </summary>
    public interface IMessageHistoryService
    {
        /// <summary>
        /// 添加消息到历史
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<int> AddMessage(MessageHistory message);
    }
}
