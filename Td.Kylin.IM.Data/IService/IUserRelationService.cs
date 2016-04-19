using System.Threading.Tasks;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 用户关系数据服务接口
    /// </summary>
    public interface IUserRelationService
    {
        /// <summary>
        /// 检测两个用户之间是否存在关联（系）
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        /// <returns></returns>
        bool HasRelation(long firstUser, long secondUser);

        /// <summary>
        /// 两个用户建立关联（系）
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        /// <returns></returns>
        Task<int> CreateRelation(long firstUser, long secondUser);
    }
}
