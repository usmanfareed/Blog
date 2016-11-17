namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAvtiveToIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Posts", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Posts", "IsActive");
        }
    }
}
