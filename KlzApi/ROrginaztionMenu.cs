using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class ROrginaztionMenu
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid AppId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid MenuId {set;get;}
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
