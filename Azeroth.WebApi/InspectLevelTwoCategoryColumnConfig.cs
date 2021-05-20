using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class InspectLevelTwoCategoryColumnConfig
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? LevelTwoCateogryCode {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int? ModuleId {set;get;}
        /// <summary>
        ///数据Key
        /// </summary>
        [StringLength(50)]
        public string ColKey {set;get;}
        /// <summary>
        ///列标题
        /// </summary>
        [StringLength(50)]
        public string Caption {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(2000)]
        public string Attributes {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(2000)]
        public string CssAttributes {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(2000)]
        public string BehaviorAttributes {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsAssignInspectorCol {set;get;}
        /// <summary>
        ///是否检查类别列
        /// </summary>
        public bool IsInspectContentCategoryCol {set;get;}
        /// <summary>
        ///
        /// </summary>
        public bool IsInspectContentCol {set;get;}
        /// <summary>
        ///是否详情页显示项
        /// </summary>
        public bool IsDisplayInItemDetailCol {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(2000)]
        public string InnerHtml {set;get;}
        /// <summary>
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///是否分数项，否决情况
        /// </summary>
        public bool IsScoreCol {set;get;}
        /// <summary>
        ///
        /// </summary>
        public int SortCode {set;get;}
    }
}
