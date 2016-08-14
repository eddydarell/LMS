namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ClassSchema_ID", "dbo.ClassSchemas");
            DropIndex("dbo.AspNetUsers", new[] { "ClassSchema_ID" });
            CreateTable(
                "dbo.ClassSchemaApplicationUsers",
                c => new
                    {
                        ClassSchema_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ClassSchema_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.ClassSchemas", t => t.ClassSchema_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ClassSchema_ID)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.AspNetUsers", "ClassSchema_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ClassSchema_ID", c => c.Int());
            DropForeignKey("dbo.ClassSchemaApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassSchemaApplicationUsers", "ClassSchema_ID", "dbo.ClassSchemas");
            DropIndex("dbo.ClassSchemaApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ClassSchemaApplicationUsers", new[] { "ClassSchema_ID" });
            DropTable("dbo.ClassSchemaApplicationUsers");
            CreateIndex("dbo.AspNetUsers", "ClassSchema_ID");
            AddForeignKey("dbo.AspNetUsers", "ClassSchema_ID", "dbo.ClassSchemas", "ID");
        }
    }
}
