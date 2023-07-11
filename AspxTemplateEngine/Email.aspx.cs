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

public partial class Email : System.Web.UI.Page
{
    public System.Collections.Generic.List<int> MyProperty { get; set; }
    public string Name { get; set; }

    public int Age { get; set; }
    protected override void OnLoad(EventArgs e)
    {
        this.Name = this.Request["name"];
        this.Age = int.Parse(this.Request["age"]);

        this.MyProperty = System.Linq.Enumerable.Range(1, 10).ToList();
        
    }
}
