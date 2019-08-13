using System;
using System.ComponentModel.DataAnnotations;

namespace Azeroth.WebApi
{
    public partial class PublicRole0806
    {
        /// <summary>
        ///
        /// </summary>
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string RoleCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int RoleType {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int LevelerId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int Rank {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsUnique {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid CategoryId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int PermissionGroupId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid? DepartmentId {set;get;}
    }
}
