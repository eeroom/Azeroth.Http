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
           
            
        }
    }
}