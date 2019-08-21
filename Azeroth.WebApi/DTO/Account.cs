using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Azeroth.WebApi.DTO
{
    public class LoginInput
    {
        [Required(ErrorMessage = "必须指定账号")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "必须指定密码")]
        public string Pwd { get; set; }
    }
}