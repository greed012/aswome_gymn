namespace awsome_gymn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class class_fieldadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "UserId" });
            AddColumn("dbo.Attendances", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Attendances", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Attendances", "UserId");
            CreateIndex("dbo.Attendances", "ClassId");
            AddForeignKey("dbo.Attendances", "ClassId", "dbo.Classes", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "ClassId", "dbo.Classes");
            DropIndex("dbo.Attendances", new[] { "ClassId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            AlterColumn("dbo.Attendances", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Attendances", "ClassId");
            CreateIndex("dbo.Attendances", "UserId");
            AddForeignKey("dbo.Attendances", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
