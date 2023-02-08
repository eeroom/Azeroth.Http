using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlzNextApi
{
    public class TreeNode
    {
        public string id { get; set; }

        public string text { get; set; }

        public List<TreeNode> children { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string pid { get; set; }

        public string state { get; set; }
    }
}
