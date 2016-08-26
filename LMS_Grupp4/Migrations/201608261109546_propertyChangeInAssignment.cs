namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertyChangeInAssignment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "Score", c => c.Double());
            AlterColumn("dbo.Assignments", "MaxScore", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assignments", "MaxScore", c => c.Int(nullable: false));
            AlterColumn("dbo.Assignments", "Score", c => c.Int());
        }
    }
}
