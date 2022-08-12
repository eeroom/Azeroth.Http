using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicRole
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///角色Code，机构内唯一
        /// </summary>
        [Required]
        [StringLength(100)]
        public string RoleCode {set;get;}
        /// <summary>
        ///岗位名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///枚举，1-实际岗位，2-虚拟岗位
        /// </summary>
        public int RoleType {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int LevelerId {set;get;}
        /// <summary>
        ///枚举,1-单位一把手
        /// </summary>
        public int Rank {set;get;}
        /// <summary>
        ///是否唯一,true-唯一  false-不唯一
        /// </summary>
        public bool IsUnique {set;get;}
        /// <summary>
        ///机构Code
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
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
        ///机构类型Id（模板专用）
        /// </summary>
        public Guid CategoryId {set;get;}
        /// <summary>
        ///所属权限组
        /// </summary>
        public int PermissionGroupId {set;get;}
        /// <summary>
        ///所属部门
        /// </summary>
        public Guid? DepartmentId {set;get;}
    }
}
