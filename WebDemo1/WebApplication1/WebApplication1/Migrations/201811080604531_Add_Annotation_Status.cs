namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Annotation_Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomModels", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomModels", "Status");
        }
    }
}
