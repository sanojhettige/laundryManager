namespace LaundryManagerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPayemnt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaidAmount", c => c.Decimal());
            AddColumn("dbo.Orders", "PaidNote", c => c.String(nullable: true, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaidAmount");
            DropColumn("dbo.Orders", "PaidNote");
        }
    }
}
