using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicOrganization
    {
        /// <summary>
        ///主键，全局唯一
        /// </summary>
        [Required]
        [StringLength(100)]
        [Key]
        public string OrgCode {set;get;}
        /// <summary>
        ///机构名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///所属省
        /// </summary>
        [StringLength(100)]
        public string Province {set;get;}
        /// <summary>
        ///所属城市
        /// </summary>
        [StringLength(100)]
        public string City {set;get;}
        /// <summary>
        ///地址
        /// </summary>
        [StringLength(500)]
        public string Address {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int LevelerId {set;get;}
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///数据库连接字符串
        /// </summary>
        [StringLength(100)]
        public string ConnStr {set;get;}
        /// <summary>
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///机构类型Id
        /// </summary>
        public Guid CategoryId {set;get;}
        /// <summary>
        ///父级机构
        /// </summary>
        [StringLength(100)]
        public string ParentCode {set;get;}
        /// <summary>
        ///简称
        /// </summary>
        [StringLength(100)]
        public string ShortName {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(4000)]
        public string OwnerDepartmentRange {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsOwnerDepartmentSpecified {set;get;}
    }
}
