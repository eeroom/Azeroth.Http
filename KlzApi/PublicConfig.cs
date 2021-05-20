using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicConfig
    {
        /// <summary>
        ///主键Id
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///枚举|PublicConfigType|
        /// </summary>
        public int ConfigType {set;get;}
        /// <summary>
        ///值
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Value {set;get;}
        /// <summary>
        ///描述
        /// </summary>
        [StringLength(500)]
        public string Description {set;get;}
        /// <summary>
        ///
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid CreatetorId {set;get;}
    }
}
