using ex4.Models;

namespace ex4.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBook(int id);
        Book CreateBook(Book book);
        Book UpdateBook(int id, Book updatedBook);
        void DeleteBook(int id);
        List<Book> SelectBookByPublishedYear(int year);
    }
}
