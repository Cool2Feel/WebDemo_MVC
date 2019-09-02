namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoryModels", "Person", c => c.String());
            AddColumn("dbo.RoomModels", "Person", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomModels", "Person");
            DropColumn("dbo.HistoryModels", "Person");
        }
    }
}
