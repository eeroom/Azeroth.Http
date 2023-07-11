using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxTemplateEngine
{
    /// <summary>
    /// 解决中文乱码
    /// </summary>
    public class WorkerRequest4Utf8 : System.Web.Hosting.SimpleWorkerRequest
    {
        private System.IO.TextWriter Output;
        public WorkerRequest4Utf8(string a1, string a2, System.IO.TextWriter a3) : base(a1, a2, a3)
        {
            Output = a3;
        }
        public override void SendResponseFromMemory(byte[] data, int length)
        {
            Output.Write(System.Text.Encoding.UTF8.GetChars(data, 0, length));
        }
    }
}
