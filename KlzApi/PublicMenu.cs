using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicMenu
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///所属系统
        /// </summary>
        public Guid AppId {set;get;}
        /// <summary>
        ///名称（界面）
        /// </summary>
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///父级Id
        /// </summary>
        public Guid? ParentId {set;get;}
        /// <summary>
        ///备注
        /// </summary>
        [StringLength(500)]
        public string Remark {set;get;}
        /// <summary>
        ///css代码
        /// </summary>
        [StringLength(4000)]
        public string CssAttributes {set;get;}
        /// <summary>
        ///背景图片
        /// </summary>
        [StringLength(500)]
        public string BackgroundImgSrc {set;get;}
        /// <summary>
        ///图标
        /// </summary>
        [StringLength(200)]
        public string Icon {set;get;}
        /// <summary>
        ///资源地址
        /// </summary>
        [StringLength(200)]
        public string Url {set;get;}
        /// <summary>
        ///排序标识
        /// </summary>
        public int SortCode {set;get;}
        /// <summary>
        ///枚举，1-菜单，2-非菜单
        /// </summary>
        public int Category {set;get;}
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///
        /// </summary>
        [StringLength(200)]
        public string KeyWords {set;get;}
    }
}
