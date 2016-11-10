namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeChangesToPostModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Slug", c => c.String());
            AddColumn("dbo.Posts", "Content", c => c.String());
            AddColumn("dbo.Posts", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdatedAt", c => c.DateTime());
            DropColumn("dbo.Posts", "Text");
            DropColumn("dbo.Posts", "Image");
            DropColumn("dbo.Posts", "CreatedOn");
            DropColumn("dbo.Posts", "DeletedData");
            DropColumn("dbo.Posts", "SourceCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "SourceCode", c => c.String());
            AddColumn("dbo.Posts", "DeletedData", c => c.String());
            AddColumn("dbo.Posts", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "Image", c => c.String());
            AddColumn("dbo.Posts", "Text", c => c.String());
            DropColumn("dbo.Posts", "UpdatedAt");
            DropColumn("dbo.Posts", "DeletedAt");
            DropColumn("dbo.Posts", "CreatedAt");
            DropColumn("dbo.Posts", "Content");
            DropColumn("dbo.Posts", "Slug");
        }
    }
}
