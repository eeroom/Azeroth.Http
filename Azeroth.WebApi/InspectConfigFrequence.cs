using System;
using System.ComponentModel.DataAnnotations;

namespace Azeroth.WebApi
{
    public partial class InspectConfigFrequence
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///枚举|TimeType|
        /// </summary>
        public int TimeType {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? Numbers {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? LevelOneCategoryCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(200)]
        public string Description {set;get;}
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? LevelerId {set;get;}
    }
}
