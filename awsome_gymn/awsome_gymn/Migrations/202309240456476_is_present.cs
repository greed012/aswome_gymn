namespace awsome_gymn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class is_present : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "IsPresent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "IsPresent");
        }
    }
}
