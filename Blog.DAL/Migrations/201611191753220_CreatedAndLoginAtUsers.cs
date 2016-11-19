namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAndLoginAtUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FullName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "LoginAt", c => c.DateTime());
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Users", "LoginAt");
            DropColumn("dbo.Users", "FullName");
        }
    }
}
