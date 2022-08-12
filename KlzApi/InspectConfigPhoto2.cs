using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigPhoto2
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
        public int InspectorLevelerId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int InspectTargetLevelerId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsPhotoNeeded {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(500)]
        public string Description {set;get;}
    }
}
