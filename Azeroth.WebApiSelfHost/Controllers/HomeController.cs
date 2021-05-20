using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
namespace SelfHostApi.Controllers
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
