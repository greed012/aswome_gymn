namespace awsome_gymn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendance_module_added_controller_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TrainingSessionId = c.Int(nullable: false),
                        AttendanceDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainingSessions", t => t.TrainingSessionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TrainingSessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "TrainingSessionId", "dbo.TrainingSessions");
            DropIndex("dbo.Attendances", new[] { "TrainingSessionId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            DropTable("dbo.Attendances");
        }
    }
}
