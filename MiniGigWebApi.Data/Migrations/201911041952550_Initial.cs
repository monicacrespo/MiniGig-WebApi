namespace MiniGigWebApi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        GigDate = c.DateTime(nullable: false),
                        MusicGenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MusicGenres", t => t.MusicGenreId, cascadeDelete: true)
                .Index(t => t.MusicGenreId);
            
            CreateTable(
                "dbo.MusicGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "MusicGenreId", "dbo.MusicGenres");
            DropIndex("dbo.Gigs", new[] { "MusicGenreId" });
            DropTable("dbo.MusicGenres");
            DropTable("dbo.Gigs");
        }
    }
}
