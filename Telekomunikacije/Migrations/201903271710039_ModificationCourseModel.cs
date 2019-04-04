namespace Telekomunikacije.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificationCourseModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Courses", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Password", c => c.String());
            AlterColumn("dbo.Courses", "Name", c => c.String());
        }
    }
}
