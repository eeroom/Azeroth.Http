using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OA
{
    public partial class _default : System.Web.UI.Page
    {
        public List<ProcessSheet> LstProcessSheet { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            var oadbcontext = new OADbContext();
            var container = oadbcontext.CreateContainer();
             var processsheet= oadbcontext.Set<ProcessSheet>(container)
                .Select(x=>new {
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
            container.WHERE = processsheet.Col(x => x.CurrentHandler) == this.Context.User.Identity.Name;
            this.LstProcessSheet= container.ToList<ProcessSheet>();
            this.rptProcessSheet.DataSource = this.LstProcessSheet;
            this.rptProcessSheet.DataBind();
        }

        protected string GetCategory(ProcessSheet processSheet)
        {
            if (string.IsNullOrEmpty(processSheet.Tag))
                return processSheet.Category;
            return $"{processSheet.Category}({processSheet.Tag})";
        }
    }
}