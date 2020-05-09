namespace Odh.BooksDemo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            AddColumn("dbo.Book", "AuthorName", c => c.String(nullable: false));
            AddColumn("dbo.Book", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "AuthorId");
            AddForeignKey("dbo.Book", "AuthorId", "dbo.Author", "AuthorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropColumn("dbo.Book", "AuthorId");
            DropColumn("dbo.Book", "AuthorName");
            DropTable("dbo.Author");
        }
    }
}
