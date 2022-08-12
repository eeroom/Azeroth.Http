using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigAssessment
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
        ///枚举|AssessmentType|
        /// </summary>
        public int AssessmentType {set;get;}
        /// <summary>
        ///枚举|PulishmentType|
        /// </summary>
        public int PulishmentType {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? ResultStaus {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? RectificationFeedbackCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? From {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? To {set;get;}
        /// <summary>
        ///
        /// </summary>
        public Decimal MultipleTimes {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(50)]
        public string Description {set;get;}
    }
}
