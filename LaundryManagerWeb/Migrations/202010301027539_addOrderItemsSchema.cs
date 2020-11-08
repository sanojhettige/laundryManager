namespace LaundryManagerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOrderItemsSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OrderId = c.Int(nullable: false),
                    UserId = c.String(),
                    ProductId = c.Int(nullable: false),
                    Quantity = c.Int(),
                    UnitPrice = c.Decimal(),
                    Notes = c.String(),
                    CreatedAt = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderItems");
        }
    }
}
