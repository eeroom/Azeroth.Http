using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicPermissionGroup
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int Id {set;get;}
        /// <summary>
        ///所属机构
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
        /// <summary>
        ///所属大类
        /// </summary>
        public int? LevelOneCategoryCode {set;get;}
        /// <summary>
        ///名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///默认设置
        /// </summary>
        public int? DefaultPermissionGroupId {set;get;}
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
        ///对应归口部门分组
        /// </summary>
        public int? CategoryOwnerGroupId {set;get;}
    }
}
