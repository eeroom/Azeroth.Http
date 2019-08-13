using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace Azeroth.WebApi.Controllers
{
    public class HomeController:System.Web.Http.ApiController
    {
        public TopDbContext TopDbContext { set; get; }

        [HttpGet]
        public List<InspectConfigResultStatus> GetEntities([FromUri] InspectConfigResultStatus parameter)
        {
            var lst= this.TopDbContext.InspectConfigResultStatuses.Take(10).ToList();
            return lst;
        }
    }
}