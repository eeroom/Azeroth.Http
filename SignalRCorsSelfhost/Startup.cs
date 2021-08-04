using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using Microsoft.Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Collections.Generic;
namespace SignalRCorsSelfhost
{

    public class Startup {
        public void Configuration(Owin.IAppBuilder app) {
            //跨域设置
            var co = CorsOptions.AllowAll;
            //解决跨域，缺少响应头，context.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT");
            //解决跨域，缺少响应头，context.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");;
            //导致每次都要option方法的问题
            co.CorsEngine = new MyCorsEngine();
            app.UseCors(co);
            //路由注册(使用默认)
            app.MapSignalR();
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("htt", "{controller}/{action}");
            app.UseWebApi(config);
        }

    }

    public class MyCorsEngine: CorsEngine {
        public override bool TryValidateMethod(CorsRequestContext requestContext, CorsPolicy policy, CorsResult result) {
            var rt= base.TryValidateMethod(requestContext, policy, result);
            var dict= result.ToResponseHeaders();
            return rt;
        }

        public override CorsResult EvaluatePolicy(CorsRequestContext requestContext, CorsPolicy policy) {
            //这里是影响Access-Control-Max-Age响应头
            policy.PreflightMaxAge = 1728000;
            var mq= base.EvaluatePolicy(requestContext, policy);
            return new MyCorsResult(mq);
        }
    }

    public class MyCorsResult: CorsResult {
        private CorsResult cr;

        public MyCorsResult(CorsResult cr) {
            this.cr = cr;
        }
        public override IDictionary<string, string> ToResponseHeaders() {
            var dict= this.cr.ToResponseHeaders();
            if (!dict.ContainsKey(CorsConstants.AccessControlAllowMethods)) {
                //这里是影响Access-Control-Allow-Methods响应头
                dict.Add(CorsConstants.AccessControlAllowMethods, "GET, POST, PUT");
            }
            return dict;
        }
    }
}