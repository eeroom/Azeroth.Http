using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebServiceClient
{
    public partial class HomeClient : System.Web.UI.Page
    {
        public string Message { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            var urlHome= System.Configuration.ConfigurationManager.AppSettings["urlHome"];
            var clientProxy = (IHome)new AsmxClientProxy<IHome>(urlHome).GetTransparentProxy();
            this.Message=clientProxy.HelloWorld(3);
        }
    }
}