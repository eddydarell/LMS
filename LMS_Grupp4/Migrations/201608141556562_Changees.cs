namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changees : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LMSFiles", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LMSFiles", "Name", c => c.String(nullable: false, maxLength: 45));
        }
    }
}
