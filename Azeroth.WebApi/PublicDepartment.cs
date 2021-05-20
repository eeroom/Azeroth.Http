using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicDepartment
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///机构内唯一
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DepartmentCode {set;get;}
        /// <summary>
        ///所属机构，god创建的记录为null
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
        /// <summary>
        ///名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///层级
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
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///机构类别
        /// </summary>
        public Guid CategoryId {set;get;}
    }
}
