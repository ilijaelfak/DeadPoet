namespace Telekomunikacije.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexNumberToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IndexNumber", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IndexNumber");
        }
    }
}
