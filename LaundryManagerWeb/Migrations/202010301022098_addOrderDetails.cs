namespace LaundryManagerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOrderDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerName", c => c.String());
            AddColumn("dbo.Orders", "CustomerPhone", c => c.String());
            AddColumn("dbo.Orders", "PickUpPerson", c => c.String());
            AddColumn("dbo.Orders", "PickUpPersonPhone", c => c.String(nullable: true, maxLength: 255));
            AddColumn("dbo.Orders", "PickUpDateTime", c => c.DateTime());

        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerName");
            DropColumn("dbo.Orders", "CustomerPhone"); 
            DropColumn("dbo.Orders", "PickUpPerson");
            DropColumn("dbo.Orders", "PickUpPersonPhone");
            DropColumn("dbo.Orders", "PickUpDateTime");
        }
    }
}
