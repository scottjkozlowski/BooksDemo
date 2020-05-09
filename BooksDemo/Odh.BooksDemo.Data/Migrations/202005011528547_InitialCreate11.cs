namespace Odh.BooksDemo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.Book", new[] { "AuthorId" });
            AlterColumn("dbo.Book", "AuthorId", c => c.Int());
            CreateIndex("dbo.Book", "AuthorId");
            AddForeignKey("dbo.Book", "AuthorId", "dbo.Author", "AuthorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.Book", new[] { "AuthorId" });
            AlterColumn("dbo.Book", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "AuthorId");
            AddForeignKey("dbo.Book", "AuthorId", "dbo.Author", "AuthorId", cascadeDelete: true);
        }
    }
}
