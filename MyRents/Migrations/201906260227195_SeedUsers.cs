namespace MyRents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            // Creating a migration with the guest and manager account
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8a3ae2a7-e17b-461c-9879-90ff4c8531f2', N'admin@myrents.ca', 0, N'AJWgylT9uiw90EbyXOVC2M8vD4NLBAcSzEAW3U7ZD4rEoBYZumf2O5SH27sN+xCYhw==', N'f8f7c910-d007-46c8-a6ac-6a7d357e7620', NULL, 0, 0, NULL, 1, 0, N'admin@myrents.ca')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd4dd6def-85ba-44c8-8434-48076d182212', N'guest@myrents.ca', 0, N'AB/QPyINa05yOc9NsqCsWUkhQk8q/w7at9ez8+d0KUCCvO4us613Y+qH0hM6QMgZ5w==', N'e04f9481-5be5-4e0d-af1b-014c8966e5ea', NULL, 0, 0, NULL, 1, 0, N'guest@myrents.ca')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5c7f13a9-b043-4957-921a-7e47dd05b941', N'canManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8a3ae2a7-e17b-461c-9879-90ff4c8531f2', N'5c7f13a9-b043-4957-921a-7e47dd05b941')

");
        }
        
        public override void Down()
        {
        }
    }
}
