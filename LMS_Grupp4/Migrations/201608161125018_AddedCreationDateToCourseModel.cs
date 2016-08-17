namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreationDateToCourseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CreationDate");
        }
    }
}
