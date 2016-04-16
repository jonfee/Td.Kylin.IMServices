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
        /// 未读消息数量
        /// </summary>
        public int UnreadCount { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
