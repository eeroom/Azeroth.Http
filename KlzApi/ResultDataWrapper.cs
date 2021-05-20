using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlzApi
{
    public class ResultDataWrapper
    {
        public System.Net.HttpStatusCode code { get; set; }

        public string msg { get; set; }

        public string errormsg { get; set; }

        public string token { get;private set; }

        public void SetToken(string token)
        {
            this.token = token;
        }

        public static ResultDataWrapper Create(System.Net.HttpStatusCode code, string msg, string errormsg)
        {
            ResultDataWrapper resultData = new ResultDataWrapper() {
                code = code,
                errormsg = errormsg,
                msg = msg
            };
            return resultData;
        }

        public static ResultDataWrapper<T> Create<T>(System.Net.HttpStatusCode code, string msg, string errormsg, T data)
        {
            ResultDataWrapper<T> resultData = new ResultDataWrapper<T>() {
                code = code,
                errormsg = errormsg,
                msg = msg,
                data = data
            };
            return resultData;
        }

        public static ResultDataWrapper<T> Ok<T>(T value)
        {
            return Create(System.Net.HttpStatusCode.OK, string.Empty, string.Empty, value);
        }
        public static ResultDataWrapper Ok()
        {
            return Create(System.Net.HttpStatusCode.OK, string.Empty, string.Empty);
        }

        public static ResultDataWrapper Error(string msg,string errormsg)
        {
            return Create(System.Net.HttpStatusCode.InternalServerError, msg, errormsg);
        }
    }
    public class ResultDataWrapper<T> : ResultDataWrapper
    {
        public T data { get; set; }
    }
}