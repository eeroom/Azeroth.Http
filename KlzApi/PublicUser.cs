using System;
using System.ComponentModel.DataAnnotations;

namespace KlzApi
{
    public partial class PublicUser
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///姓名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name {set;get;}
        /// <summary>
        ///用户编号
        /// </summary>
        [StringLength(100)]
        public string UserNumber {set;get;}
        /// <summary>
        ///所属机构的Code
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OrgCode {set;get;}
        /// <summary>
        ///所属部门的Code
        /// </summary>
        [StringLength(100)]
        public string DepartmentCode {set;get;}
        /// <summary>
        ///登陆名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LoginName {set;get;}
        /// <summary>
        ///密码，可以解密
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Password {set;get;}
        /// <summary>
        ///手机号码
        /// </summary>
        [StringLength(100)]
        public string Phone {set;get;}
        /// <summary>
        ///电子邮箱
        /// </summary>
        [StringLength(100)]
        public string Email {set;get;}
        /// <summary>
        ///枚举，性别，1-男，2-女
        /// </summary>
        public int? Gender {set;get;}
        /// <summary>
        ///
        /// </summary>
        public DateTime? Birthdate {set;get;}
        /// <summary>
        ///排序标识
        /// </summary>
        public int SortCode {set;get;}
        /// <summary>
        ///枚举|RowActive|，1-正常，2-已删除
        /// </summary>
        public int Active {set;get;}
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDateTime {set;get;}
        /// <summary>
        ///创建人
        /// </summary>
        public Guid CreatetorId {set;get;}
        /// <summary>
        ///true-离职，false-在职
        /// </summary>
        public bool IsResigned {set;get;}
    }
}
