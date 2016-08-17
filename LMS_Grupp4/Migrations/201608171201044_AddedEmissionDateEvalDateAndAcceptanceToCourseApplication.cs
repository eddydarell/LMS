namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmissionDateEvalDateAndAcceptanceToCourseApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseApplications", "IsAccepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourseApplications", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CourseApplications", "EvaluationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseApplications", "EvaluationDate");
            DropColumn("dbo.CourseApplications", "CreationDate");
            DropColumn("dbo.CourseApplications", "IsAccepted");
        }
    }
}
