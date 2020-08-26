using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Azeroth.WcfTwoWayAuthentication
{
    public partial class Client : System.Web.UI.Page
    {
        public string DateValue { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            using (var factory = new System.ServiceModel.ChannelFactory<IHome>("wch"))
            {
                var client = factory.CreateChannel();

                this.DateValue = client.DoWork();
            }
        }
    }
}