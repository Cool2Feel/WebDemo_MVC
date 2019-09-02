namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Page2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomModels", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomModels", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
