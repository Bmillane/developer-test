namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViewingsModelsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Viewings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ViewingTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Property_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.Property_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Property_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viewings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Viewings", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Viewings", new[] { "Property_Id" });
            DropIndex("dbo.Viewings", new[] { "UserId" });
            DropTable("dbo.Viewings");
        }
    }
}
