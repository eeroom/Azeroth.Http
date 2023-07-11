using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OA
{
    public partial class askforleave : System.Web.UI.Page
    {
        public string Category { get; set; }

        public string LeavType { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Reason { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (this.Request.HttpMethod.ToLower() != "post")
                return;
            this.Category = this.Request["Category"];
            this.LeavType = this.Request["LeavType"];
            this.Reason = this.Request["Reason"];
            if (string.IsNullOrEmpty(this.Category) || string.IsNullOrEmpty(this.LeavType))
                return;
            DateTime startTime;
            if (!DateTime.TryParse(this.Request["StartTime"], out startTime))
                return;
            this.StartTime = startTime;
            DateTime endTime;
            if (!DateTime.TryParse(this.Request["EndTime"], out endTime))
                return;
            this.EndTime = endTime;
            ProcessSheet psheet = new ProcessSheet();
            psheet.Category = this.Category;
            psheet.Creator = this.Context.User.Identity.Name;
            psheet.CreatTime = DateTime.Now;
            psheet.CurrentHandler = this.Context.User.Identity.Name;
            psheet.Status = "处理中";
            psheet.WorkFlowId = Guid.NewGuid();
            psheet.Remark = this.LeavType;
            psheet.Formdata = Newtonsoft.Json.JsonConvert.SerializeObject(new { this.LeavType, this.StartTime, this.EndTime, this.Reason });
            var dbcontext = new OADbContext();
            var cud= dbcontext.Cud<ProcessSheet>();
            cud.Select(x => new
            {
                x.Category,
                x.Creator,
                x.CreatTime,
                x.CurrentHandler,
                x.Formdata,
                x.Remark,
                x.Status,
                x.WorkFlowId
            });
            cud.Insert(psheet);
            var rt= dbcontext.SaveChange(cud);
            this.Response.Redirect("mylist.aspx", true);
        }
    }
}