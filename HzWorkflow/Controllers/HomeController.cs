using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HzWorkflow.Controllers
{
    public class HomeController: System.Web.Http.ApiController
    {

        public Student GetInfo()
        {
            var st = new Student() { Age = 101, Name = "张三" };
            return st;
        }
    }
}