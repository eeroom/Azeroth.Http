using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace Azeroth.WebApi.Controllers
{
    public class HomeController:HaishanController
    {
        

        [HttpGet]
        public ResultDataWrapper<List<InspectConfigResultStatus>> GetEntities([FromUri] InspectConfigResultStatus parameter)
        {
            var lst= this.DbContext.InspectConfigResultStatuses.Take(1).ToList();
            if (DateTime.Now.Second%2==0)
                throw new HaishanException("测试异常");
            lst.ForEach(x => x.ResultName = this.CurrentUser.UserInfo.Name);
            return ResultDataWrapper.Ok(lst);
        }
    }
}