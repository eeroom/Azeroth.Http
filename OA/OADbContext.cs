using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA
{
    public class OADbContext:Azeroth.Nalu.DbContext<System.Data.SqlClient.SqlConnection>
    {
        public OADbContext()
        {
            this.Cnnstr = "Data Source=.\\DEV;Initial Catalog=OA;Integrated Security=False;User ID=sa;Password=123456;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}