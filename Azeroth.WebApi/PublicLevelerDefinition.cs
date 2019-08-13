using System;
using System.ComponentModel.DataAnnotations;

namespace Azeroth.WebApi
{
    public partial class PublicLevelerDefinition
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public int Id {set;get;}
        /// <summary>
        ///机构类别
        /// </summary>
        [Key]
        public Guid CategoryId {set;get;}
        /// <summary>
        ///名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///true-允许有部门，false-不能包含部门
        /// </summary>
        public bool HasDepartment {set;get;}
        /// <summary>
        ///能否接受下发
        /// </summary>
        public bool CanAcceptDistributionCategory {set;get;}
        /// <summary>
        ///父级lever
        /// </summary>
        public int? SuperiorLevelId {set;get;}
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
        ///是否有信息管理员
        /// </summary>
        public bool HasAdmin {set;get;}
        /// <summary>
        ///格式长度-用户确定orgcode
        /// </summary>
        public int FormatLength {set;get;}
    }
}
