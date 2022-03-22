using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateEngine
{
    public class SimpleHost : MarshalByRefObject
    {
        public void ProcessRequest(string page,string query, System.IO.Stream stream)
        {
            var wt = new System.IO.StreamWriter(stream);
            TemplateRequest swr = new TemplateRequest(page, query, wt);
            System.Web.HttpRuntime.ProcessRequest(swr);
            wt.Flush();
        }
    }
}
