using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace OA
{

    public sealed class WorkFlowEnd : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<Guid> Gid { get; set; }


        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            ProcessSheet psheet = new ProcessSheet();
            psheet.Status = "已完成";
            psheet.CurrentHandler = string.Empty;
            var dbcontext = new OADbContext();
            var cud = dbcontext.Cud<ProcessSheet>();
            var gid= this.Gid.Get(context);
            cud.WHERE = cud.Col(x => x.GId) == gid;
            cud.Update(psheet);
            cud.Select(x => new { x.Status, x.CurrentHandler });
            dbcontext.SaveChange(cud);
        }
    }
}
