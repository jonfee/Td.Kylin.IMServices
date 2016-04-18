using System;
using System.Threading.Tasks;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 错误日志数据服务接口
    /// </summary>
    public interface IErrorLogService
    {
        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="ex"><seealso cref="Exception"/></param>
        /// <param name="tag"></param>
        Task<int> AddLog(Exception ex, string tag);
    }
}
