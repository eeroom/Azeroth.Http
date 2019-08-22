using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
namespace Azeroth.WebApi.Controllers
{
    public abstract class HaishanController:ApiController
    {
        public HaishanDbContext DbContext { set; get; }
    }
}