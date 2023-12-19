namespace awsome_gymn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class membership_enroll2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.membership_enrollment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MembershipId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.memberships", t => t.MembershipId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MembershipId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.membership_enrollment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.membership_enrollment", "MembershipId", "dbo.memberships");
            DropIndex("dbo.membership_enrollment", new[] { "UserId" });
            DropIndex("dbo.membership_enrollment", new[] { "MembershipId" });
            DropTable("dbo.membership_enrollment");
        }
    }
}
