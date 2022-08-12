using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace KlzApi
{
    public class HaishanDbContext : System.Data.Entity.DbContext,IScoped
    {
        public HaishanDbContext():base("name=mastermssqlserver")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            
        }
        public virtual DbSet<InspectConfigResultStatus> InspectConfigResultStatuses { set; get; }
        
    }
}