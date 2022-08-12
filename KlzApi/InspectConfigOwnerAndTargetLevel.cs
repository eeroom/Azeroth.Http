using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigOwnerAndTargetLevel
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
        public bool IsMultipleOwnerAllowed {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsInspectorAnyLevel {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(50)]
        public string InspectorLevel {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool? IsInspectorSameLevelAsUser {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsInspectTargetAnyLevel {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(50)]
        public string InspectTargetLevel {set;get;}
    }
}
