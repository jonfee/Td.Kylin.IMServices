using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Td.Kylin.IM.Data.Entity
{
    /// <summary>
    /// 用户登录记录
    /// </summary>
    [Table("UserLoginRecords")]
    public class UserLoginRecords
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public long RecordID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 登录时所在地ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 登录所在地名称（如：广东省深圳市）
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 登录经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 登录纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 终端设备类型（枚举：TerminalDevice）
        /// </summary>
        public int TerminalDevice { get; set; }

        //登录时间
        public DateTime LoginTime { get; set; }
    }
}
