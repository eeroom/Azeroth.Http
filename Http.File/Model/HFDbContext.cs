using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Http.File.Model {
    public class HFDbContext:DbContext {
        static HFDbContext() {
            Database.SetInitializer<HFDbContext>(null);
        }

        public HFDbContext():base("name=hz") {

        }

        public DbSet<FileEntity> FileEntity { get; set; }
    }
}