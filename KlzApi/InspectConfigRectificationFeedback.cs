using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigRectificationFeedback
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
        public int FeedbackCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Feedback {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsNeeded {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CustomFeedbackCaption {set;get;}
    }
}
