using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectPhaseForDb
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///阶段名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PhaseName {set;get;}
        /// <summary>
        ///阶段值
        /// </summary>
        public int PhaseValue {set;get;}
    }
}
