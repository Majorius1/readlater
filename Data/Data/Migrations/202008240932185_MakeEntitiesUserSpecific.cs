namespace ReadLater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeEntitiesUserSpecific : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookmarks", "AuthorId", c => c.String(maxLength: 128));
            AddColumn("dbo.Categories", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookmarks", "AuthorId");
            CreateIndex("dbo.Categories", "AuthorId");
            AddForeignKey("dbo.Bookmarks", "AuthorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Categories", "AuthorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Categories", new[] { "AuthorId" });
            DropIndex("dbo.Bookmarks", new[] { "AuthorId" });
            DropColumn("dbo.Categories", "AuthorId");
            DropColumn("dbo.Bookmarks", "AuthorId");
        }
    }
}
