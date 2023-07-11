using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA {
    public class OrginazitionNode {
        public Guid Id { get; set; }

        public Guid Pid { get; set; }

        public string Name { get; set; }

        public OrginazitionNodeType Category { get; set; }

    }
}
