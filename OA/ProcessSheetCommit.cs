using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA
{
    public class ProcessSheetCommit
    {
        public Guid Id { get; set; }

        public int SheetId { get; set; }

        public string HandlerName { get; set; }

        public DateTime HandlerTime { get; set; }


        public string HandlerValue { get; set; }
    }
}