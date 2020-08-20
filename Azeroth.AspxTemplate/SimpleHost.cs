using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azeroth.AspxTemplate
{
    public class SimpleHost : MarshalByRefObject
    {
        public string ProcessRequest(string fileName, System.IO.Stream stream)
        {
            var wt = new System.IO.StreamWriter(stream);
            TemplateRequest swr = new TemplateRequest(fileName, "", wt);
            
            System.Web.HttpRuntime.ProcessRequest(swr);
            stream.Position = 0;
            var reader = new System.IO.StreamReader(stream);
            var rt = reader.ReadToEnd();
            return rt;
        }
    }
}
