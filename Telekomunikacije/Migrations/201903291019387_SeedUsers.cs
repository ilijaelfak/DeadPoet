namespace Telekomunikacije.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9e24397d-0dd2-49f9-8273-3cb688ad7a8e', N'guest@gmail.com', 0, N'AA46WvJOWUGlxafIRsBGfnY2SbvYl3kyNirvwkR4EIe+CRthmgwN257TK1U5DZzRMA==', N'e50d33c5-44ba-49cc-96a5-b7b411dad166', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ae1bc23f-4d9e-4c96-9d47-057a2a1f14c4', N'admin@telekomunikacije.com', 0, N'AFNO59FbktB9deoR1GeGMvVWVSG3Mzlrpdicab3B+xwGa5LpcWr4yerxsQ4p+2HzIA==', N'17ef1514-3c73-41fd-b2ca-afdf2f8cbc1f', NULL, 0, 0, NULL, 1, 0, N'admin@telekomunikacije.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'79e667f5-7756-4c4b-a0d4-83e89301f1c1', N'CanDoEverything')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ae1bc23f-4d9e-4c96-9d47-057a2a1f14c4', N'79e667f5-7756-4c4b-a0d4-83e89301f1c1')


");
        }
        
        public override void Down()
        {
        }
    }
}
