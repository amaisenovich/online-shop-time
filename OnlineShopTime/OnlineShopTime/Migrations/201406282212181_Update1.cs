namespace OnlineShopTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Claim", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ExternalLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersRoles", "UserId", "dbo.Users");
            DropIndex("dbo.ExternalLogins", new[] { "UserId" });
            DropIndex("dbo.UsersRoles", new[] { "UserId" });
            DropIndex("dbo.Claim", new[] { "User_Id" });
            RenameColumn(table: "dbo.Claim", name: "User_Id", newName: "IdentityUser_Id");
            DropPrimaryKey("dbo.ExternalLogins");
            AddColumn("dbo.Users", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Users", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Claim", "UserId", c => c.String());
            AddColumn("dbo.ExternalLogins", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UsersRoles", "IdentityUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Claim", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ExternalLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("dbo.Roles", "Name", unique: true, name: "RoleNameIndex");
            CreateIndex("dbo.UsersRoles", "IdentityUser_Id");
            CreateIndex("dbo.Claim", "IdentityUser_Id");
            CreateIndex("dbo.ExternalLogins", "IdentityUser_Id");
            AddForeignKey("dbo.Claim", "IdentityUser_Id", "dbo.Users", "UserID");
            AddForeignKey("dbo.ExternalLogins", "IdentityUser_Id", "dbo.Users", "UserID");
            AddForeignKey("dbo.UsersRoles", "IdentityUser_Id", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.ExternalLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.Claim", "IdentityUser_Id", "dbo.Users");
            DropIndex("dbo.ExternalLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Claim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UsersRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropPrimaryKey("dbo.ExternalLogins");
            AlterColumn("dbo.Claim", "IdentityUser_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
            DropColumn("dbo.UsersRoles", "IdentityUser_Id");
            DropColumn("dbo.ExternalLogins", "IdentityUser_Id");
            DropColumn("dbo.Claim", "UserId");
            DropColumn("dbo.Users", "AccessFailedCount");
            DropColumn("dbo.Users", "LockoutEnabled");
            DropColumn("dbo.Users", "LockoutEndDateUtc");
            DropColumn("dbo.Users", "TwoFactorEnabled");
            DropColumn("dbo.Users", "PhoneNumberConfirmed");
            DropColumn("dbo.Users", "EmailConfirmed");
            AddPrimaryKey("dbo.ExternalLogins", new[] { "UserId", "LoginProvider", "ProviderKey" });
            RenameColumn(table: "dbo.Claim", name: "IdentityUser_Id", newName: "User_Id");
            CreateIndex("dbo.Claim", "User_Id");
            CreateIndex("dbo.UsersRoles", "UserId");
            CreateIndex("dbo.ExternalLogins", "UserId");
            AddForeignKey("dbo.UsersRoles", "UserId", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.ExternalLogins", "UserId", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Claim", "User_Id", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
