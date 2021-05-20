using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigPhoto
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? LevelOneCategoryCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? ResultStatus {set;get;}
        /// <summary>
        ///用户自定义名称
        /// </summary>
        [StringLength(100)]
        public string CustomResultName {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? FeedbackCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(100)]
        public string Feedback {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsPhotoNeeded {set;get;}
        /// <summary>
        ///枚举|InspectPhase|
        /// </summary>
        public int? Phase {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(50)]
        public string Description {set;get;}
    }
}
