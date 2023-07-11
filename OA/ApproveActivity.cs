using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace OA{

    public sealed class ApproveActivity : NativeActivity {
        public InArgument<HzWorkFlowContext> HzWfContext { get; set; }

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

        protected override bool CanInduceIdle {
            get {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context) {
            var bookmark = this.BookMark.Get(context);
            Console.WriteLine(bookmark+":通过各项参数找到审批人，为该审批人添加一条审批任务，并向他发送通知");
          

            //Program.bookmark = bookmark;
            //Program.workflowId = context.WorkflowInstanceId;
            context.CreateBookmark(bookmark, OnApproveCommit);

        }

        private void OnApproveCommit(NativeActivityContext context, Bookmark bookmark, object value) {
            var flag = (bool)value;
            //转审操作由审批页面的业务代码处理，不进入工作流，本质就是对被转审人增加一条审批任务
            if (flag) {
                Console.WriteLine("审批通过，流程继续");
            } else {
                Console.WriteLine("审批驳回，流程终止");
                //做一些补偿操作，记录驳回的流程实例，后续利用terminal方法删除
                context.Abort(new ArgumentException("审批驳回，流程终止"));
            }

        }
    }
}
