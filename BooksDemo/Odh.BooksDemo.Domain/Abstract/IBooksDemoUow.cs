namespace Odh.BooksDemo.Domain.Abstract
{
    public interface IBooksDemoUow
    {
        void Commit();
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
    }
}
