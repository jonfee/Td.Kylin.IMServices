using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Td.Kylin.IM.Data.Entity
{
    /// <summary>
    /// 错误日志
    /// </summary>
    [Table("ErrorLog")]
    public class ErrorLog
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public long LogID { get; set; }

        /// <summary>
        /// 自定义标识信息
        /// </summary>
        public string Tag { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Source { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Message { get; set; }

        [Column(TypeName = "text")]
        public string StackTrace { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string HelpLink { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
