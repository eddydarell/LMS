namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCOurse_ApplicationViewModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseApplications", "Message", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseApplications", "Message", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
