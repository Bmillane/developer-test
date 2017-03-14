namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Offers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Offers", "UserId");
            RenameColumn(table: "dbo.Offers", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.Offers", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Offers", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Offers", "UserId");
            AddForeignKey("dbo.Offers", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Offers", new[] { "UserId" });
            AlterColumn("dbo.Offers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Offers", "UserId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Offers", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Offers", "UserId", c => c.String(nullable: false));
            CreateIndex("dbo.Offers", "ApplicationUser_Id");
            AddForeignKey("dbo.Offers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
