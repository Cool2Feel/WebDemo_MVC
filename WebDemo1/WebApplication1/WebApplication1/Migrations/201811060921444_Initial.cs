namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomModels", "Tips", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomModels", "Tips", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
