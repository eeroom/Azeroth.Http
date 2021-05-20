using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostOwinApi.Controllers
{
    public class HomeController:ApiController
    {
        [HttpGet]
        public List<int> GetEntities()
        {
            var lst = System.Linq.Enumerable.Range(0, 10).ToList();
            return lst;
        }
    }
}
