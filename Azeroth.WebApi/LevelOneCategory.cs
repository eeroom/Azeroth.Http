using System;
using System.ComponentModel.DataAnnotations;

namespace Azeroth.WebApi
{
    public partial class LevelOneCategory
    {
        /// <summary>
        ///主键
        /// </summary>
        [Key]
        public int Code {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public DateTime CreateTime {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OwnerDepartmentCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(4000)]
        public string AvailableDepartmentCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(4000)]
        public string AvailableLeveler {set;get;}
        /// <summary>
        ///不能直接使用该大类，但需要配合检查的机构
        /// </summary>
        [StringLength(4000)]
        public string AvailableLevelerOnlyForInspect {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(4000)]
        public string AvailableUserCode {set;get;}
        /// <summary>
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int SortCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool AllowMultipleOwners {set;get;}
    }
}
