using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Td.Kylin.IM.Data.Entity
{
    /// <summary>
    /// IM 用户
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户账号身份类型（枚举：UserType）
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string Photo { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public int Status { get; set; }

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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
