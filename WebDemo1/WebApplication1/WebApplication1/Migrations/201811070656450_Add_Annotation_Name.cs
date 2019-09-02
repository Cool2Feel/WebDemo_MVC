namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Annotation_Name : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomModels", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomModels", "Name", c => c.String());
        }
    }
}
