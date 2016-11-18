namespace Blog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDeletedAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "DeletedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "DeletedAt", c => c.DateTime());
        }
    }
}
