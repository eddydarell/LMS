namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTitleToClassSChema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassSchemas", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassSchemas", "Title");
        }
    }
}
