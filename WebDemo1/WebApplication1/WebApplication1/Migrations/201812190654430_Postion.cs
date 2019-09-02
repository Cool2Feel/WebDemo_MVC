namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Postion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomModels", "Number", c => c.String());
            AddColumn("dbo.RoomModels", "Postion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomModels", "Postion");
            DropColumn("dbo.RoomModels", "Number");
        }
    }
}
