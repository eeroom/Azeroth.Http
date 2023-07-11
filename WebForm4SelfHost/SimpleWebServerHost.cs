﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebForm4SelfHost
{
    class SimpleWebServerHost:System.MarshalByRefObject
    {
        public void ProcessRequest(Uri url, System.IO.Stream stream)
        {
            string file = url.LocalPath.Trim('/');
            using (var wt = new System.IO.StreamWriter(stream))
            {
                System.Web.Hosting.SimpleWorkerRequest swr = new System.Web.Hosting.SimpleWorkerRequest(file, "", wt);
                System.Web.HttpRuntime.ProcessRequest(swr);
            }
        }
    }
}
