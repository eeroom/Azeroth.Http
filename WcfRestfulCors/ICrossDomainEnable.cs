﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfRestfulCors
{
    [System.ServiceModel.ServiceContract]
    public interface ICrossDomainEnable
    {
        [System.ServiceModel.OperationContract]
        [System.ServiceModel.Web.WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void OptionsHandler();
    }
}
