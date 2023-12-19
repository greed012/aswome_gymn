namespace awsome_gymn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class class_created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                        Group = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                        FbLink = c.String(),
                        InstaLink = c.String(),
                        TwitterLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingSessions", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.TrainingSessions", "ClassId", "dbo.Classes");
            DropIndex("dbo.TrainingSessions", new[] { "TrainerId" });
            DropIndex("dbo.TrainingSessions", new[] { "ClassId" });
            DropTable("dbo.Trainers");
            DropTable("dbo.TrainingSessions");
            DropTable("dbo.Classes");
        }
    }
}
