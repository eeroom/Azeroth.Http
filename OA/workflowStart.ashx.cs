using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA
{
    /// <summary>
    /// workflowStart 的摘要说明
    /// </summary>
    public class workflowStart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            ProcessSheet psheet = new ProcessSheet();
            psheet.Formdata = context.Request["formdata"];
            var formdata = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(psheet.Formdata);
           
            psheet.Category = formdata["Category"]?.ToString();
            psheet.Creator = context.User.Identity.Name;
            psheet.CreatTime = DateTime.Now;
            psheet.CurrentHandler = context.User.Identity.Name;
            psheet.Status = "处理中";
            psheet.WorkFlowId = Guid.Empty;
            psheet.Tag = formdata[formdata["TagField"].ToString()]?.ToString();
            var dbcontext = new OADbContext();
            var cud = dbcontext.Cud<ProcessSheet>();
            cud.Select(x => new
            {
                x.Category,
                x.Creator,
                x.CreatTime,
                x.CurrentHandler,
                x.Formdata,
                x.Tag,
                x.Status,
                x.WorkFlowId
            });
            cud.Insert(psheet);
            var rt = dbcontext.SaveChange(cud);
            if (rt < 1)
                return;
            System.Xaml.XamlXmlReaderSettings st = new System.Xaml.XamlXmlReaderSettings()
            {
                LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly()
            };
            System.Activities.Activity act = null;
            using (var reader = new System.Xaml.XamlXmlReader(formdata["WorkFlowXaml"].ToString(), st))
            {
                act = System.Activities.XamlIntegration.ActivityXamlServices.Load(reader);
            }
            
            var wfa = new System.Activities.WorkflowApplication(act, formdata);
            ConfigWfa(wfa);
            wfa.Run();
            context.Response.Redirect("mylist.aspx", true);
        }

        private  void ConfigWfa(System.Activities.WorkflowApplication wfa)
        {
            var cnnstr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["OA"].ConnectionString;
            //配置工作流持久化的数据库
            //创建数据库，执行2个脚本C:\Windows\Microsoft.NET\Framework64\v4.0.30319\SQL\zh-Hans\SqlWorkflowInstanceStoreSchema.sql
            //C:\Windows\Microsoft.NET\Framework64\v4.0.30319\SQL\zh-Hans\SqlWorkflowInstanceStoreLogic.sql
            wfa.InstanceStore = new System.Activities.DurableInstancing.SqlWorkflowInstanceStore(cnnstr);

            wfa.Aborted = AbortedHandler;
            wfa.Completed = CompletedHandler;
            wfa.Idle = IdleHandler;
            wfa.OnUnhandledException = OnUnhandledExceptionHandler;
            wfa.PersistableIdle = PersistableIdleHandler;
            wfa.Unloaded = UnloadedHandler;
        }

        private  void UnloadedHandler(WorkflowApplicationEventArgs obj)
        {
            Console.WriteLine("UnloadedHandler");
        }

        private  PersistableIdleAction PersistableIdleHandler(WorkflowApplicationIdleEventArgs arg)
        {
            Console.WriteLine("PersistableIdleHandler");
            return PersistableIdleAction.Unload;
        }

        private  UnhandledExceptionAction OnUnhandledExceptionHandler(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("OnUnhandledExceptionHandler");
            return UnhandledExceptionAction.Abort;
        }

        private  void IdleHandler(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("IdleHandler");
        }

        private  void CompletedHandler(WorkflowApplicationCompletedEventArgs obj)
        {
            Console.WriteLine("CompletedHandler");
        }

        private  void AbortedHandler(WorkflowApplicationAbortedEventArgs obj)
        {
            Console.WriteLine("AbortedHandler");
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