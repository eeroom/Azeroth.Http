using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient
{
    public class InvokedException:Exception
    {
        public InvokedException(string message,Exception innerException):base(message,innerException)
        {

        }
        public string Url { get; set; }


        public object Parameter { get; set; }

        public string Action { get; set; }

        


    }
}
