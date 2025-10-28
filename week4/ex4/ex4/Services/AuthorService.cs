using ex4.Data;
using ex4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex4.Services
{
    public class AuthorService
    {
        private readonly LibraryContext _context;
        public AuthorService(LibraryContext context)
        {
            _context = context;
        }
        public List<Author> GetAllAuthors()
        {
            return _context.Authors
                .Include(a=>a.Books)
                .ToList();
        }
        public Author GetAuthorById(int id)
        {
            var author = _context.Authors
                .Include(a => a.Books)
                .FirstOrDefault(x => x.Id == id);

            if (author == null)
            {
                throw new Exception("Указанный автор не существует");
            }
            return author;
        }

        public Author CreateAuthor(Author author)
        {
            if (string.IsNullOrEmpty(author.Name))
            {
                throw new Exception("Имя автора обязательно");
            }

            if (author.Books != null && author.Books.Any())
            {
                foreach (var book in author.Books)
                {
                    book.Id = 0;
                    book.Author = author;
                }
            }

            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }
       
        public Author UpdateAuthor(int id, Author updateAuthor)
        {
            var existAuthor = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (existAuthor == null)
            {
                throw new Exception("Указанный автор не существует");
            }

            if (string.IsNullOrEmpty(updateAuthor.Name))
            {
                throw new Exception("Имя автора обязательно");
            }

            existAuthor.Name = updateAuthor.Name;
            existAuthor.DateOfBirth = updateAuthor.DateOfBirth;
            _context.SaveChanges();
            return existAuthor;
        }

        
        public void DeleteAuthor(int id)
        {
            var existAuthor = _context.Authors
                .Include(a => a.Books)
                .FirstOrDefault(x => x.Id == id);

            if (existAuthor == null)
            {
                throw new Exception("Указанный автор не существует");
            }

            _context.Authors.Remove(existAuthor);
            _context.SaveChanges();
        }

        public List<object> GetAuthorWithBookCount()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении авторов с количеством книг: {ex.Message}");
            }
        }

        public List<Author> FindAuthorsByName(string name)
        {
            try
            {
                return _context.Authors
                    .Where(a => a.Name.Contains(name))
                    .Include(a => a.Books)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при поиске авторов по имени '{name}': {ex.Message}");
            }
        }
    }
}
