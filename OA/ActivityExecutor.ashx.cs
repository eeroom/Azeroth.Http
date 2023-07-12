using System;
using System.Activities;
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
            var oadbcontext = new OADbContext();
            var container = oadbcontext.CreateContainer();
            var processsheetSet = oadbcontext.Set<ProcessSheet>(container)
               .Select(x => new { x.WorkFlowId, x.WorkFlowXaml,x.Bookmark });
            container.WHERE = processsheetSet.Col(x => x.Id) == psheetId && processsheetSet.Col(x=>x.CurrentHandler)==context.User.Identity.Name;
            var processSheet = container.ToList<ProcessSheet>().FirstOrDefault();
            if (processsheetSet == null)
                throw new ArgumentException("处理失败，指定的流程可能已经完成当前结点");

            System.Xaml.XamlXmlReaderSettings st = new System.Xaml.XamlXmlReaderSettings()
            {
                LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly()
            };
            System.Activities.Activity act = null;
            var xamlfilePath = System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, processSheet.WorkFlowXaml);
            using (var reader = new System.Xaml.XamlXmlReader(xamlfilePath, st))
            {
                act = System.Activities.XamlIntegration.ActivityXamlServices.Load(reader);
            }
            var wfa = new System.Activities.WorkflowApplication(act);
            ConfigWfa(wfa);
            wfa.Load(processSheet.WorkFlowId);
            var dict = new Dictionary<string, string>();
            dict.Add("approve", context.Request["approve"]);
            wfa.ResumeBookmark(processSheet.Bookmark, dict);
            context.Response.Redirect("default.aspx");

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void ConfigWfa(System.Activities.WorkflowApplication wfa)
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

        private void UnloadedHandler(WorkflowApplicationEventArgs obj)
        {
            Console.WriteLine("UnloadedHandler");
        }

        private PersistableIdleAction PersistableIdleHandler(WorkflowApplicationIdleEventArgs arg)
        {
            Console.WriteLine("PersistableIdleHandler");
            return PersistableIdleAction.Unload;
        }

        private UnhandledExceptionAction OnUnhandledExceptionHandler(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("OnUnhandledExceptionHandler");
            return UnhandledExceptionAction.Abort;
        }

        private void IdleHandler(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("IdleHandler");
        }

        private void CompletedHandler(WorkflowApplicationCompletedEventArgs obj)
        {
            Console.WriteLine("CompletedHandler");
        }

        private void AbortedHandler(WorkflowApplicationAbortedEventArgs obj)
        {
            Console.WriteLine("AbortedHandler");
        }
    }
}