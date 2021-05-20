using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectStandardItemException
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int LevelOneCategoryCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int LevelTwoCategoryCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid ItemId {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
    }
}
