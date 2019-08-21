using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Azeroth.WebApi.Controllers
{
    [HaishanAuthorizationFilter]
    public abstract class HaishanController: System.Web.Http.ApiController
    {
        public UserInfoWrapper CurrentUser { get; set; }

        public HaishanDbContext DbContext { set; get; }
    }
}