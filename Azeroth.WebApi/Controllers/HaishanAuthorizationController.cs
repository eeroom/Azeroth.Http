using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace KlzApi.Controllers
{
    [HaishanAuthorizationFilter]
    public abstract class HaishanAuthorizationController : HaishanController
    {
        public UserInfoWrapper CurrentUser { get; set; }

        
    }
}