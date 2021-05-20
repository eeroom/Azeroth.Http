using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectConfigSelectTargetOrg
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///大类code
        /// </summary>
        public int LevelerOneCategoryCode {set;get;}
        /// <summary>
        ///检查人层级
        /// </summary>
        public int LevelerId {set;get;}
        /// <summary>
        ///false-不需要被检查单位，true-需要被检查单位
        /// </summary>
        public bool IsSelectTargetOrgNeeded {set;get;}
    }
}
