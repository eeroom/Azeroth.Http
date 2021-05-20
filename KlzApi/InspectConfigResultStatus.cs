using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigResultStatus
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///一级分类code
        /// </summary>
        public int? LevelOneCategoryCode {set;get;}
        /// <summary>
        ///枚举|InspectPhase|检查阶段
        /// </summary>
        public int Phase {set;get;}
        /// <summary>
        ///枚举|InspectPhaseActionType|操作类型
        /// </summary>
        public int? ActionType {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string StandardResultName {set;get;}
        /// <summary>
        ///是否需要
        /// </summary>
        public bool IsNeeded {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(100)]
        public string CustomResultName {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ResultName {set;get;}
        /// <summary>
        ///实际对应的值
        /// </summary>
        public int ResultStatus {set;get;}
        /// <summary>
        ///枚举|InspectPhase|
        /// </summary>
        public int? ItemDisplayedInThisPhase {set;get;}
        /// <summary>
        ///是否必须整改评价
        /// </summary>
        public bool IsRectficationReedbackNeeded {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Guid LastUpdaterId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public DateTime LastUpdaterDatetime {set;get;}
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
