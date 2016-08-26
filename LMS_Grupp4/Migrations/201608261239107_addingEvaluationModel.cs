namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingEvaluationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Mark = c.String(maxLength: 2),
                        Message = c.String(maxLength: 144),
                        IsPassed = c.Boolean(nullable: false),
                        Score = c.Double(),
                        Percentage = c.Double(),
                        Assignment_ID = c.Int(),
                        LMSFile_ID = c.Int(),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assignments", t => t.Assignment_ID)
                .ForeignKey("dbo.LMSFiles", t => t.LMSFile_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.Assignment_ID)
                .Index(t => t.LMSFile_ID)
                .Index(t => t.Student_Id);
            
            DropColumn("dbo.Assignments", "Mark");
            DropColumn("dbo.Assignments", "IsPassed");
            DropColumn("dbo.Assignments", "Score");
            DropColumn("dbo.Assignments", "Percentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Percentage", c => c.Double());
            AddColumn("dbo.Assignments", "Score", c => c.Double());
            AddColumn("dbo.Assignments", "IsPassed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Assignments", "Mark", c => c.String(maxLength: 2));
            DropForeignKey("dbo.Evaluations", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Evaluations", "LMSFile_ID", "dbo.LMSFiles");
            DropForeignKey("dbo.Evaluations", "Assignment_ID", "dbo.Assignments");
            DropIndex("dbo.Evaluations", new[] { "Student_Id" });
            DropIndex("dbo.Evaluations", new[] { "LMSFile_ID" });
            DropIndex("dbo.Evaluations", new[] { "Assignment_ID" });
            DropTable("dbo.Evaluations");
        }
    }
}
