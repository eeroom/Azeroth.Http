using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace ZamApi.Controllers
{
    public class HomeController:ControllerBase
    {
        private readonly Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider;

        public HomeController(Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            this.apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
        }
        [HttpGet]
        public List<int> GetEntities()
        {
            var lstApi = this.apiDescriptionGroupCollectionProvider.ApiDescriptionGroups;
            var lst = System.Linq.Enumerable.Range(0, 10).ToList();
            return lst;
        }
    }
}
