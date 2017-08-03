namespace Web11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureFKComment : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            AlterColumn("dbo.Comments", "ParentComment_Id", c => c.Int());
            CreateIndex("dbo.Comments", "ParentComment_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            AlterColumn("dbo.Comments", "ParentComment_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ParentComment_Id");
        }
    }
}
