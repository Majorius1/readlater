namespace ReadLater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookmarkUsageCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookmarks", "UsageCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookmarks", "UsageCount");
        }
    }
}
