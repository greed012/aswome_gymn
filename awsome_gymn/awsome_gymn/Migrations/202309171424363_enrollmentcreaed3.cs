namespace awsome_gymn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollmentcreaed3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "TrainingSessionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrollments", "TrainingSessionId");
            AddForeignKey("dbo.Enrollments", "TrainingSessionId", "dbo.TrainingSessions", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "TrainingSessionId", "dbo.TrainingSessions");
            DropIndex("dbo.Enrollments", new[] { "TrainingSessionId" });
            DropColumn("dbo.Enrollments", "TrainingSessionId");
        }
    }
}
