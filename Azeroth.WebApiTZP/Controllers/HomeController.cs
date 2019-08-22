using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace Azeroth.WebApiTZP.Controllers
{
    public class HomeController:ControllerBase
    {
        [HttpGet]
        public List<int> GetEntities()
        {
            var lst = System.Linq.Enumerable.Range(0, 10).ToList();
            return lst;
        }
    }
}
