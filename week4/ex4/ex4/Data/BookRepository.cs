using ex4.Models;
using Microsoft.EntityFrameworkCore;

namespace ex4.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public List<Book> GetAllWithAuthors()
        {
            return _context.Books
                .Include(b => b.Author)
                .ToList();
        }

        public Book GetByIdWithAuthor(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetBooksByPublishedYear(int year)
        {
            return _context.Books
                .Where(b => b.PublishedYear.Year > year)
                .Include(b => b.Author)
                .ToList();
        }

        public bool BookExists(int id)
        {
            return _context.Books.Any(b => b.Id == id);
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }
    }
}