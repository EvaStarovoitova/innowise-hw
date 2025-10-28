using ex4.Data;
using ex4.Models;

namespace ex4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRep;
        private readonly IAuthorRepository _authorRep;

        public BookService(IBookRepository bookRep, IAuthorRepository authorRep)
        {
            _bookRep = bookRep;
            _authorRep = authorRep;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRep.GetAllWithAuthors();
        }

        public Book GetBook(int id)
        {
            var book = _bookRep.GetByIdWithAuthor(id);
            if (book == null)
            {
                throw new Exception("Книга не найдена");
            }
            return book;
        }

        public Book CreateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new Exception("Название книги обязательно");
            }

            if (!_authorRep.AuthorExists(book.AuthorId))
            {
                throw new Exception("Указанный автор не существует");
            }

            book.Id = 0;
            return _bookRep.Create(book);
        }

        public Book UpdateBook(int id, Book updatedBook)
        {
            var existBook = _bookRep.GetById(id);
            if (existBook == null)
            {
                throw new Exception("Книга не найдена");
            }

            if (string.IsNullOrWhiteSpace(updatedBook.Title))
            {
                throw new Exception("Название книги обязательно");
            }

            if (!_authorRep.AuthorExists(updatedBook.AuthorId))
            {
                throw new Exception("Указанный автор не существует");
            }

            existBook.Title = updatedBook.Title;
            existBook.PublishedYear = updatedBook.PublishedYear;
            existBook.AuthorId = updatedBook.AuthorId;

            return _bookRep.Update(existBook);
        }

        public void DeleteBook(int id)
        {
            if (!_bookRep.BookExists(id))
            {
                throw new Exception("Указанной книги не существует");
            }
            _bookRep.Delete(id);
        }

        public List<Book> SelectBookByPublishedYear(int year)
        {
            return _bookRep.GetBooksByPublishedYear(year);
        }
    }
}