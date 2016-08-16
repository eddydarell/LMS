namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileModelNameChangedToLMSFile : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Files", newName: "LMSFiles");
            RenameTable(name: "dbo.FileCourses", newName: "LMSFileCourses");
            RenameColumn(table: "dbo.LMSFileCourses", name: "File_ID", newName: "LMSFile_ID");
            RenameIndex(table: "dbo.LMSFileCourses", name: "IX_File_ID", newName: "IX_LMSFile_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LMSFileCourses", name: "IX_LMSFile_ID", newName: "IX_File_ID");
            RenameColumn(table: "dbo.LMSFileCourses", name: "LMSFile_ID", newName: "File_ID");
            RenameTable(name: "dbo.LMSFileCourses", newName: "FileCourses");
            RenameTable(name: "dbo.LMSFiles", newName: "Files");
        }
    }
}
