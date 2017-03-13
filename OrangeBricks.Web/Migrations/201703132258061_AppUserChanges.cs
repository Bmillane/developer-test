namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Offers", "ApplicationUser_Id");
            AddForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Offers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Offers", "ApplicationUser_Id");
        }
    }
}
