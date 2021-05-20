using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class RPermissionGroupMenu
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///权限组Id
        /// </summary>
        public int PermissionGroupId {set;get;}
        /// <summary>
        ///菜单Id
        /// </summary>
        public Guid MenuId {set;get;}
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///系统Id
        /// </summary>
        public Guid AppId {set;get;}
        /// <summary>
        ///机构类型Id
        /// </summary>
        public Guid CategoryId {set;get;}
    }
}
