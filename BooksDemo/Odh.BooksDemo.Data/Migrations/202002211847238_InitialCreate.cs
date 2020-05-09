namespace Odh.BooksDemo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false),
                        IsbNumber = c.String(nullable: false),
                        GenreId = c.Int(nullable: false),
                        PublishedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.ELMAH_Error",
                c => new
                    {
                        ErrorId = c.Guid(nullable: false),
                        Application = c.String(nullable: false, maxLength: 60),
                        Host = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 100),
                        Source = c.String(nullable: false, maxLength: 60),
                        Message = c.String(nullable: false, maxLength: 500),
                        User = c.String(nullable: false, maxLength: 50),
                        StatusCode = c.Int(nullable: false),
                        TimeUtc = c.DateTime(nullable: false),
                        Sequence = c.Int(nullable: false, identity: true),
                        AllXml = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ELMAH_Error");
            DropTable("dbo.Book");
        }
    }
}
