namespace Telekomunikacije.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetRoles]   ([Id] ,[Name]) VALUES  ('4bc910ee-06bd-493d-8fa3-b466ad97600c'  ,'CanLearn')
            INSERT INTO [dbo].[AspNetRoles]   ([Id] ,[Name]) VALUES  ('db228122-b816-43be-b739-fc63625057f6' , 'CanMakeCourse')
            INSERT INTO [dbo].[AspNetRoles]   ([Id] ,[Name]) VALUES  ('8652d671-b1d4-48ea-8d38-82db914f55df'  ,'CanTeach')
              ");
        }
        
        public override void Down()
        {
        }
    }
}
