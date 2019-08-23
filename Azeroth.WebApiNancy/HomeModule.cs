using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
namespace Azeroth.WebApiNancy
{
    public class HomeModule:NancyModule
    {
        public HomeModule()
        {
            this.Get["/"] = r => {
                var os = System.Environment.OSVersion;
                return "Hello Nancy<br/> System:" + os.VersionString;
            };

            this.Get["/aa"] = r => {
                var os = System.Environment.OSVersion;
                return "Hello rrrrrr<br/> System:" + os.VersionString;
            };
        }
    }
}