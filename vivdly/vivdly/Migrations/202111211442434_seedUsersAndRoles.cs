namespace vivdly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsersAndRoles : DbMigration
    {
        public override void Up()
        {

            //password for both is 123456
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8c1ceca0-69a7-4dc8-bd9d-0ea0749e0086', N'guest@vivdly.com', 0, N'AOwwww+LkvskeWncRD4XTL3WQpauPXd4Td2eoF3Tu8RCfCsg6EzTzpstKXvYevo8VQ==', N'5b1e6a0f-5a64-4215-b0c9-2501ea5c3186', NULL, 0, 0, NULL, 1, 0, N'guest@vivdly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9b63a5d4-417a-4584-8eb1-eaab2ce05bcf', N'admin@vivdly.com', 0, N'AGwmgU7iVW+1o5cNzRkzum13p6JY1dFXP/HSxy49TuUiuwSGbiyoh22qdNnp3oYFJw==', N'485fed51-d6c2-4584-9fb2-ba6db5b9a041', NULL, 0, 0, NULL, 1, 0, N'admin@vivdly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c72ea6b4-a7aa-492d-a180-0c66072a73ae', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9b63a5d4-417a-4584-8eb1-eaab2ce05bcf', N'c72ea6b4-a7aa-492d-a180-0c66072a73ae')



            ");
        }
        
        public override void Down()
        {
        }
    }
}
