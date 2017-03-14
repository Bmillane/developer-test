namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OfferChangesToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Offers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Offers", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.Offers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Offers", "ApplicationUser_Id");
            AddForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Offers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Offers", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Offers", "UserId");
            CreateIndex("dbo.Offers", "ApplicationUser_Id");
            AddForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
