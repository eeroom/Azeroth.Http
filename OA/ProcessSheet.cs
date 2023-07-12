using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA
{
    public class ProcessSheet
    {
        public int Id { get; set; }

        public Guid WorkFlowId { get; set; }

        public string Category { get; set; }

        public string Creator { get; set; }

        public DateTime CreatTime { get; set; }

        public string Tag { get; set; }

        public string CurrentHandler { get; set; }

        public string Status { get; set; }

        public string Formdata { get; set; }

        public Guid GId { get; set; }

        public string Bookmark { get; set; }

        public string WorkFlowXaml { get; set; }


    }
}