using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Td.Kylin.IM.Data.Entity
{
    /// <summary>
    /// 未读消息
    /// </summary>
    [Table("UnreadMessage")]
    public class UnreadMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MessageID { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 发送者ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }

        /// <summary>
        /// 接收者ID
        /// </summary>
        public long Receiver { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string Content { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
    }
}
