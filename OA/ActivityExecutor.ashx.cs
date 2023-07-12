using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA
{
    /// <summary>
    /// ActivityExecutor 的摘要说明
    /// </summary>
    public class ActivityExecutor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var psheetId = int.Parse(context.Request["psheetId"]);
            var approve = context.Request["approve"];
            var oadbcontext = new OADbContext();
            var container = oadbcontext.CreateContainer();
            var processsheet = oadbcontext.Set<ProcessSheet>(container)
               .Select(x => new {
                   x.Category,
                   x.Creator,
                   x.CreatTime,
                   x.CurrentHandler,
                   x.Formdata,
                   x.Id,
                   x.Tag,
                   x.Status,
                   x.WorkFlowId
               });
            container.WHERE = processsheet.Col(x => x.Id) == psheetId && processsheet.Col(x=>x.CurrentHandler)==context.User.Identity.Name;
            var processSheet = container.ToList<ProcessSheet>().FirstOrDefault();
            if (processsheet == null)
                throw new ArgumentException("处理失败，指定的流程可能已经完成当前结点");
            context.Response.Redirect("default.aspx");

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}