using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace WcfRestful
{
    public class ErrorHandler : System.ServiceModel.Dispatcher.IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message msg)
        {

            ErrorInfo model = new ErrorInfo() { Message = error.Message };

            //添加将要返回的异常信息
            msg = Message.CreateMessage(version, string.Empty, model, new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ErrorInfo)));

            //告诉WCF使用JSON编码而不是默认的XML
            WebBodyFormatMessageProperty wbf = new WebBodyFormatMessageProperty(WebContentFormat.Json);

            //添加格式化程序故障。
            msg.Properties.Add(WebBodyFormatMessageProperty.Name, wbf);

            //修改响应
            //HttpResponseMessageProperty rmp = new HttpResponseMessageProperty();

            // 返回自定义错误码, 500.
            //rmp.StatusCode = System.Net.HttpStatusCode.InternalServerError;

            //rmp.StatusDescription = "Bad request";

            ////Headers写入jsonerror和JSON内容（ Content-Type 标头，指定伴随正文数据的 MIME 类型。）
            //rmp.Headers[System.Net.HttpResponseHeader.ContentType] = "application/json";
            //rmp.Headers[System.Net.HttpResponseHeader.ContentEncoding] = "utf-8";
            ////rmp.Headers["jsonerror"] = "true";

            ////添加到故障
            //msg.Properties.Add(HttpResponseMessageProperty.Name, rmp);
        }

      public   class ErrorInfo
        {
            
            public bool Result { get; set; }
            public string Message { get; set; }
        }


    }
}
