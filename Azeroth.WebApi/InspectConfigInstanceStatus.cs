using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigInstanceStatus
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int InstanceStatus {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int ResultStatus {set;get;}
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
