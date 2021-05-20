using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlzApi
{
    public class UserInfoWrapper:IScoped
    {
        public UserInfo UserInfo { get; set; }

        public JwtData JwtData { get; set; }
    }
}