namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAllday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleEvents", "IsAllDay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleEvents", "IsAllDay");
        }
    }
}
