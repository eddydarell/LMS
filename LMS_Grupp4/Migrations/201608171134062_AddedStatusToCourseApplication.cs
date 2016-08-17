namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusToCourseApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseApplications", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseApplications", "Status");
        }
    }
}
