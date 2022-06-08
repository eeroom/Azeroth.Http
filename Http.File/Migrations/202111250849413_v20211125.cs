namespace HttpFile.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20211125 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileEntity", "FullName", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.FileEntity", "ClientHashValue", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.FileEntity", "Size", c => c.Long(nullable: false));
            DropColumn("dbo.FileEntity", "Name");
            DropColumn("dbo.FileEntity", "Folder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileEntity", "Folder", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.FileEntity", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.FileEntity", "Size", c => c.Int(nullable: false));
            DropColumn("dbo.FileEntity", "ClientHashValue");
            DropColumn("dbo.FileEntity", "FullName");
        }
    }
}
