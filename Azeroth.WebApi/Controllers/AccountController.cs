using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Controllers;

namespace Azeroth.WebApi.Controllers
{
    public class AccountController:HaishanController
    {
        public UserInfoWrapper CurrentUser { get; set; }

        [HttpPost]
        public ResultDataWrapper<UserInfo> Login(DTO.LoginInput parameter)
        {
           this.CurrentUser.UserInfo= UserInfo.lstUserInfo.Where(x => x.LoginName == parameter.LoginName)
                .Where(x => x.Pwd == parameter.Pwd)
                .FirstOrDefault();
            if (this.CurrentUser.UserInfo == null)
                throw new HaishanException("用户名或密码错误");
            this.CurrentUser.JwtData = new JwtData() { Id = this.CurrentUser.UserInfo.Id, CreateDateTime=DateTime.Now };
            var token = JwtHelper.Encode(this.CurrentUser.JwtData);
            var rt= ResultDataWrapper.Ok(this.CurrentUser.UserInfo);
            rt.SetToken(token);
            return rt;
        }
    }
}