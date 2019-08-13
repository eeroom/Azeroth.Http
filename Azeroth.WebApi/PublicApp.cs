using System;
using System.ComponentModel.DataAnnotations;

namespace Azeroth.WebApi
{
    public partial class PublicApp
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///true-和大类关联，false-不和大类关联
        /// </summary>
        public bool IsCategoryRelated {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(500)]
        public string Url {set;get;}
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsPublic {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int Order {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(500)]
        public string Attributes {set;get;}
    }
}
