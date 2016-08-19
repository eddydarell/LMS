namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeEvaluationDateNullableInCourseApplication : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseApplications", "EvaluationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseApplications", "EvaluationDate", c => c.DateTime(nullable: false));
        }
    }
}
