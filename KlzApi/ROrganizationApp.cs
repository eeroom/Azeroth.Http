using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class ROrganizationApp
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///系统Id
        /// </summary>
        public Guid AppId {set;get;}
        /// <summary>
        ///机构Id
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
    }
}
