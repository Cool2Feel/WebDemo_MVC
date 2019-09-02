namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rating1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.HisTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HisTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        RoomName = c.String(),
                        Peopel = c.String(),
                        Coustomer = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Tips = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
