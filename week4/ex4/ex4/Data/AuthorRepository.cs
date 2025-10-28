using ex4.Models;
using Microsoft.EntityFrameworkCore;
namespace ex4.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly LibraryContext _context;
        public AuthorRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public List<object> GetAuthorWithBookCount()
        {
            return _context.Authors
                 .Select(a => new
                 {
                     Id = a.Id,
                     Name = a.Name,
                     DateOfBirth = a.DateOfBirth,
                     BookCount = a.Books.Count()
                 })
                 .ToList<object>();
        }
        public Author GetByIdWithBooks(int id)
        {
            return _context.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Author> FindAuthorsByName(string name)
        {
            return _context.Authors
                .Where(a => a.Name.Contains(name))
                .Include(a => a.Books)
                .ToList();
        }
        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }
    }
}
