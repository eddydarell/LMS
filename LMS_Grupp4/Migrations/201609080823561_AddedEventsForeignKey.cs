namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEventsForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleEvents", "Course_ID", "dbo.Courses");
            DropIndex("dbo.ScheduleEvents", new[] { "Course_ID" });
            RenameColumn(table: "dbo.ScheduleEvents", name: "Course_ID", newName: "CourseID");
            AlterColumn("dbo.ScheduleEvents", "CourseID", c => c.Int(nullable: false));
            CreateIndex("dbo.ScheduleEvents", "CourseID");
            AddForeignKey("dbo.ScheduleEvents", "CourseID", "dbo.Courses", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleEvents", "CourseID", "dbo.Courses");
            DropIndex("dbo.ScheduleEvents", new[] { "CourseID" });
            AlterColumn("dbo.ScheduleEvents", "CourseID", c => c.Int());
            RenameColumn(table: "dbo.ScheduleEvents", name: "CourseID", newName: "Course_ID");
            CreateIndex("dbo.ScheduleEvents", "Course_ID");
            AddForeignKey("dbo.ScheduleEvents", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
