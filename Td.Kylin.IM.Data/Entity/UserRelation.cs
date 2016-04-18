using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Td.Kylin.IM.Data.Entity
{
    /// <summary>
    /// 用户关系
    /// </summary>
    [Table("UserRelation")]
    public class UserRelation
    {
        /// <summary>
        /// 第一个用户ID
        /// </summary>
        public long FirstUser { get; set; }

        /// <summary>
        /// 第二个用户ID
        /// </summary>
        public long SecondUser { get; set; }

        /// <summary>
        /// 第一个用户删除关系
        /// </summary>
        public bool FirstDelete { get; set; }

        /// <summary>
        /// 第二个用户删除关系
        /// </summary>
        public bool SecondDelete { get; set; }

        /// <summary>
        /// 第一个用户组名（即第二个用户在第一个用户联系人里的组名称）
        /// </summary>
        public string FirstGroupName { get; set;}

        /// <summary>
        /// 第二个用户组名（即第一个用户在第二个用户联系人里的组名称）
        /// </summary>
        public string SecondGroupName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
