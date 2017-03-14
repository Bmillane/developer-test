namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOffers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            AlterColumn("dbo.Offers", "Property_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Offers", "Property_Id");
            AddForeignKey("dbo.Offers", "Property_Id", "dbo.Properties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            AlterColumn("dbo.Offers", "Property_Id", c => c.Int());
            CreateIndex("dbo.Offers", "Property_Id");
            AddForeignKey("dbo.Offers", "Property_Id", "dbo.Properties", "Id");
        }
    }
}
