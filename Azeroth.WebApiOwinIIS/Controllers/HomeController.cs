using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
namespace Azeroth.WebApiOwinIIS.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public List<int> GetEntities()
        {

            var apiExplorer= new Microsoft.Web.Http.Description.VersionedApiExplorer(this.Configuration);
            var lstApi= apiExplorer.ApiDescriptions.Flatten();
            var lst = System.Linq.Enumerable.Range(0, 10).ToList();
            return lst;
        }
    }
}