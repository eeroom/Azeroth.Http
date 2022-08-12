using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectStandardItem
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
        [StringLength(100)]
        public string StandardScore {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Item {set;get;}
        /// <summary>
        ///检查类别
        /// </summary>
        [Required]
        public string InspectContentCategory {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        public string InspectContent {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid? FromId {set;get;}
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
        public string XX {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int SortCode {set;get;}
    }
}
