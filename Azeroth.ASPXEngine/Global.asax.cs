using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

public class Global : System.Web.HttpApplication
{

    protected void Application_Start(object sender, EventArgs e)
    {


    }

    public override void Init()
    {
        this.BeginRequest += Global_BeginRequest;
    }

    private void Global_BeginRequest(object sender, EventArgs e)
    {
        Console.WriteLine("收到请求");
    }
}