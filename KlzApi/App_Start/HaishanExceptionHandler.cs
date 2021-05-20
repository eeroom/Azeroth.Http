using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Web.Http;
namespace KlzApi
{
    public class HaishanExceptionHandler : System.Web.Http.Filters.IExceptionFilter
    {
        public bool AllowMultiple { get { return false; } }

        public async Task ExecuteExceptionFilterAsync(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            string msg = "服务器忙，请联系管理员";
            var error = actionExecutedContext.Exception as HaishanException;
            if (error != null)
                msg = error.Message;
            var data = ResultDataWrapper.Error(msg, actionExecutedContext.Exception.Message);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse( System.Net.HttpStatusCode.OK,data);
        }
    }
}