using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AsmxDemo
{
    /// <summary>
    /// Home 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Home : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(int a)
        {
            return "Hello World"+a.ToString();
        }

        [WebMethod]
        public Student GetStudentById(int id) {
            return new Student() { Age = 101, Name = Guid.NewGuid().ToString() };
        }

        [WebMethod]
        public object[] Info() {
            var lst= System.Linq.Enumerable.Range(0, 10).Select(x=>(object)x).ToList();
            lst.Add("hello");
            return lst.ToArray();
        }
    }
}
