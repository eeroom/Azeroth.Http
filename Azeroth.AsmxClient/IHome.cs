﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.AsmxClient
{
    [System.Web.Services.WebServiceBinding(Namespace = "http://tempuri.org/")]
    public interface IHome
    {
        [System.Web.Services.Protocols.SoapDocumentMethod]
        string HelloWorld(int a);
    }
}