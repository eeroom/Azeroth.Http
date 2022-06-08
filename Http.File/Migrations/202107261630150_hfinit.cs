namespace HttpFile.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hfinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileEntity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Folder = c.String(nullable: false, maxLength: 255),
                        Size = c.Int(nullable: false),
                        UploadStepValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileEntity");
        }
    }
}
