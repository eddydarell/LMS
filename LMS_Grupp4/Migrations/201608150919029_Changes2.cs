namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LMSFiles", "Assignment_ID", c => c.Int());
            CreateIndex("dbo.LMSFiles", "Assignment_ID");
            AddForeignKey("dbo.LMSFiles", "Assignment_ID", "dbo.Assignments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LMSFiles", "Assignment_ID", "dbo.Assignments");
            DropIndex("dbo.LMSFiles", new[] { "Assignment_ID" });
            DropColumn("dbo.LMSFiles", "Assignment_ID");
        }
    }
}
