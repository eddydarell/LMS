namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedClassShema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassSchemaApplicationUsers", "ClassSchema_ID", "dbo.ClassSchemas");
            DropForeignKey("dbo.ClassSchemaApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClassSchemaApplicationUsers", new[] { "ClassSchema_ID" });
            DropIndex("dbo.ClassSchemaApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.ClassSchemas", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ClassSchemas", "ApplicationUser_Id");
            AddForeignKey("dbo.ClassSchemas", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ClassSchemaApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClassSchemaApplicationUsers",
                c => new
                    {
                        ClassSchema_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ClassSchema_ID, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.ClassSchemas", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ClassSchemas", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ClassSchemas", "ApplicationUser_Id");
            CreateIndex("dbo.ClassSchemaApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.ClassSchemaApplicationUsers", "ClassSchema_ID");
            AddForeignKey("dbo.ClassSchemaApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassSchemaApplicationUsers", "ClassSchema_ID", "dbo.ClassSchemas", "ID", cascadeDelete: true);
        }
    }
}
