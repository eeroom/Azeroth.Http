using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxTemplateEngine
{
    public class SimpleLocalServerHost : MarshalByRefObject
    {
        public void ProcessRequest(string page,string query, System.IO.Stream stream)
        {
            var wt = new System.IO.StreamWriter(stream);
            WorkerRequest4Utf8 swr = new WorkerRequest4Utf8(page, query, wt);
            System.Web.HttpRuntime.ProcessRequest(swr);
            wt.Flush();
        }
    }
}
