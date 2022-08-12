using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicOrganizationCategory
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
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///cos的目录
        /// </summary>
        [StringLength(100)]
        public string CosBucketName {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(100)]
        public string ThumbDomain {set;get;}
    }
}
