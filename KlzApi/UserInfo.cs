using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlzApi
{
    public class UserInfo
    {
        public static List<UserInfo> lstUserInfo = new List<UserInfo>() {
            new UserInfo() {
                Id =new Guid("320E0753-1EE2-4F4B-8919-4CB237030334"),
            LoginName ="eeroom"
        , Name="光影", Pwd="123"
} };
        public string LoginName { get; set; }

        public string Name { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Pwd { get; set; }

        public Guid Id { get; set; }
    }
}