namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Description", c => c.String());
            AddColumn("dbo.Files", "IsPublicVisible", c => c.Boolean(nullable: false));
            DropColumn("dbo.Files", "PublicVisibility");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "PublicVisibility", c => c.Boolean(nullable: false));
            DropColumn("dbo.Files", "IsPublicVisible");
            DropColumn("dbo.Courses", "Description");
        }
    }
}
