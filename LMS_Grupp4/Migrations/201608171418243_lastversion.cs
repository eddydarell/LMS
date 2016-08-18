namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastversion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseApplications", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourseApplications", "IsAccepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourseApplications", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CourseApplications", "EvaluationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CourseApplications", "Message", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseApplications", "Message", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.CourseApplications", "EvaluationDate");
            DropColumn("dbo.CourseApplications", "CreationDate");
            DropColumn("dbo.CourseApplications", "IsAccepted");
            DropColumn("dbo.CourseApplications", "Status");
        }
    }
}
