using ex4.Models;

namespace ex4.Data
{
    public interface IBookRepository: IRepository<Book>
    {
        List<Book> GetAllWithAuthors();
        Book GetByIdWithAuthor(int id);
        List<Book> GetBooksByPublishedYear(int year);
        bool BookExists(int id);
        bool AuthorExists(int authorId);
    }
}
