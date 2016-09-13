namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        Periodicity = c.Int(nullable: false),
                        EventNature = c.Int(nullable: false),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .Index(t => t.Course_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleEvents", "Course_ID", "dbo.Courses");
            DropIndex("dbo.ScheduleEvents", new[] { "Course_ID" });
            DropTable("dbo.ScheduleEvents");
        }
    }
}
