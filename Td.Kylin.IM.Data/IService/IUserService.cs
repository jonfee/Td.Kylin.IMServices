using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Td.Kylin.IM.Data.Model;

namespace Td.Kylin.IM.Data.IService
{
    /// <summary>
    /// 用户数据服务接口
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Dictionary<long, string> GetUserName(long[] userIds);

        /// <summary>
        /// 更新用户最后登录信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="name"></param>
        /// <param name="userType"></param>
        /// <param name="lastLoginAddress"></param>
        /// <param name="lastLoginTime"></param>
        /// <returns></returns>
        Task<int> UpdateLastInfo(long userID, string name, int userType, string lastLoginAddress, DateTime lastLoginTime);

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserLoginInfo GetUserLoginInfo(long userID);
    }
}
