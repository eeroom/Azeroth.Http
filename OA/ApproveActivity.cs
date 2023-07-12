using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace OA{

    public sealed class ApproveActivity : NativeActivity {

        /// <summary>
        /// 找审批人的模式
        /// </summary>
        public FindApproverMode FindMode { get; set; }

        /// <summary>
        /// 直接指定审批人（特定模式下）
        /// </summary>
        public InArgument<string> SpecialApprover { get; set; }

        /// <summary>
        /// 直接指定组织节点（特定模式下）
        /// </summary>
        public InArgument<string> SpecialNode { get; set; }

        /// <summary>
        /// 审批人的角色
        /// </summary>
        public InArgument<WorkFlowRole> ApproverRole { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public InArgument<string> ShenQingRen { get; set; }

        /// <summary>
        /// 书签
        /// </summary>
        public InArgument<string> BookMark { get; set; }

        public InArgument<Guid> Gid { get; set; }

        protected override bool CanInduceIdle {
            get {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context) {
            ProcessSheet psheet = new ProcessSheet();
            psheet.GId = this.Gid.Get(context);
            psheet.WorkFlowId = context.WorkflowInstanceId;
            psheet.Bookmark = this.BookMark.Get(context);
            psheet.Status = this.DisplayName;

            var role= this.ApproverRole.Get(context);
            if (role == WorkFlowRole.主管)
                psheet.CurrentHandler = "wch";
            else if (role == WorkFlowRole.总经理)
                psheet.CurrentHandler = "lili";

            var dbcontext = new OADbContext();
            var cud = dbcontext.Cud<ProcessSheet>();
            cud.WHERE = cud.Col(x => x.GId) == psheet.GId;
            cud.Select(x => new
            {
                x.CurrentHandler,
                x.Status,
                x.Bookmark,
                x.WorkFlowId
            });
            cud.Update(psheet);
            dbcontext.SaveChange(cud);
            context.CreateBookmark(psheet.Bookmark, this.OnApproveCommit);

        }

        private void OnApproveCommit(NativeActivityContext context, Bookmark bookmark, object value) {
           
            var dbcontext = new OADbContext();
            var container= dbcontext.CreateContainer();
            var processSheetSet= dbcontext.Set<ProcessSheet>(container);
            processSheetSet.Select(x => new { x.Id,x.CurrentHandler});
            var gid = this.Gid.Get(context);
            container.WHERE = processSheetSet.Col(x => x.GId) == gid;
            var processSheet= container.ToList<ProcessSheet>().First();

            var psc = new ProcessSheetCommit();
            psc.Id = Guid.NewGuid();
            psc.HandlerTime = DateTime.Now;
            psc.SheetId = processSheet.Id;
            psc.HandlerName = processSheet.CurrentHandler;

            var dict = (Dictionary<string, string>)value;
            psc.HandlerValue = dict["approve"];

             var cud= dbcontext.Cud<ProcessSheetCommit>();
            cud.Select(x => new { x.Id, x.SheetId, x.HandlerValue, x.HandlerTime, x.HandlerName });
            cud.Insert(psc);
            var lstcud = new List<Azeroth.Nalu.ICud>();
            lstcud.Add(cud);
            if(psc.HandlerValue== "驳回")
            {
                processSheet.CurrentHandler = string.Empty;
                processSheet.Status = $"{this.DisplayName}({psc.HandlerValue})";
                var cudps = dbcontext.Cud<ProcessSheet>();
                cudps.WHERE = cudps.Col(x => x.GId) == gid;
                cudps.Select(x => new { x.CurrentHandler, x.Status });
                cudps.Update(processSheet);
                lstcud.Add(cudps);
            }
            dbcontext.SaveChange(lstcud.ToArray());
            //转审操作由审批页面的业务代码处理，不进入工作流，本质就是对被转审人增加一条审批任务
            if (psc.HandlerValue == "驳回") {
                context.Abort(new ArgumentException("审批驳回，流程终止"));
            }
        }
    }
}
