namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addArchiveTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArchiveGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        GenreId = c.Int(),
                        DeveloperId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId)
                .Index(t => t.DeveloperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArchiveGames", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.ArchiveGames", "DeveloperId", "dbo.Developers");
            DropIndex("dbo.ArchiveGames", new[] { "DeveloperId" });
            DropIndex("dbo.ArchiveGames", new[] { "GenreId" });
            DropTable("dbo.ArchiveGames");
        }
    }
}
