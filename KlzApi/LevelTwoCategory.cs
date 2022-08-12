using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class LevelTwoCategory
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int Code {set;get;}
        /// <summary>
        ///所属大类
        /// </summary>
        public int LevelOneCategoryCode {set;get;}
        /// <summary>
        ///名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
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
