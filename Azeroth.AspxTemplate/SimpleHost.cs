using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azeroth.AspxTemplate
{
    public class SimpleHost : MarshalByRefObject
    {
        public void ProcessRequest(string fileName, System.IO.Stream stream)
        {
            var wt = new System.IO.StreamWriter(stream);
            TemplateRequest swr = new TemplateRequest(fileName, "", wt);
            System.Web.HttpRuntime.ProcessRequest(swr);
        }
    }
}
