using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateEngine
{
    /// <summary>
    /// 解决中文乱码
    /// </summary>
    public class TemplateRequest : System.Web.Hosting.SimpleWorkerRequest
    {
        private System.IO.TextWriter Output;
        public TemplateRequest(string a1, string a2, System.IO.TextWriter a3) : base(a1, a2, a3)
        {
            Output = a3;
        }
        public override void SendResponseFromMemory(byte[] data, int length)
        {
            Output.Write(System.Text.Encoding.UTF8.GetChars(data, 0, length));
        }
    }
}
