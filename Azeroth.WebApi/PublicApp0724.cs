using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicApp0724
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
        public string Name {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsCategoryRelated {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(500)]
        public string Url {set;get;}
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
