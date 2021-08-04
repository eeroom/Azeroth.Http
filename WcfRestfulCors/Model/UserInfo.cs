using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfRestfulCors.Model
{

    public class UserInfo
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<int> Number { set; get; }

        public UserInfo Parent { set; get; }
    }
}
