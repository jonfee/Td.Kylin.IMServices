using System;

namespace Td.Kylin.IM.Data.Model
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class UserLoginInfo
    {
        public long UserID { get; set; }

        /// <summary>
        /// 上一次登录时间
        /// </summary>
        public DateTime PrevLoginTime { get; set; }

        /// <summary>
        /// 上一次登录地点
        /// </summary>
        public string PrevLoginAddress { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后一次登录地点
        /// </summary>
        public string LastLoginAddress { get; set; }
    }
}
