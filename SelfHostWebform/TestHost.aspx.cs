using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class TestHost : System.Web.UI.Page
{
    public System.Collections.Generic.List<int> MyProperty { get; set; }
    protected override void OnLoad(EventArgs e)
    {
        this.MyProperty = System.Linq.Enumerable.Range(1, 100).ToList();
        
    }
}
