namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIdInAuthToken : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "RoleUsers");
            RenameColumn(table: "dbo.AuthTokens", name: "User_Id", newName: "UserId_Id");
            RenameIndex(table: "dbo.AuthTokens", name: "IX_User_Id", newName: "IX_UserId_Id");
            DropPrimaryKey("dbo.RoleUsers");
            AddPrimaryKey("dbo.RoleUsers", new[] { "Role_Id", "User_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RoleUsers");
            AddPrimaryKey("dbo.RoleUsers", new[] { "User_Id", "Role_Id" });
            RenameIndex(table: "dbo.AuthTokens", name: "IX_UserId_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.AuthTokens", name: "UserId_Id", newName: "User_Id");
            RenameTable(name: "dbo.RoleUsers", newName: "UserRoles");
        }
    }
}
