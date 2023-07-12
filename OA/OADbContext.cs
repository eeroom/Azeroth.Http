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
            this.Cnnstr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["OA"].ConnectionString;
        }
    }
}