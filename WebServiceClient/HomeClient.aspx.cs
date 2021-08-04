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


        protected void Button1_Click(object sender, EventArgs e)
        {
            var urlHome = System.Configuration.ConfigurationManager.AppSettings["urlHome"];
            var client = AsmxClient<IHome>.Create(urlHome);
            this.Message = client.HelloWorld(3);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            AzTbc.HomeSoapClient client = new AzTbc.HomeSoapClient();
            this.Message = client.HelloWorld(4);

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            AzWlk.Home home = new AzWlk.Home();
            this.Message = home.HelloWorld(5);
        }
    }
}