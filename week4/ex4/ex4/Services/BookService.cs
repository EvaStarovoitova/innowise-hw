using ex4.Data;
using ex4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex4.Services
{
    public class BookService
    {
        private readonly LibraryContext _context;

        public BookService (LibraryContext context)
        {
            _context = context;
        }
        public List<Book> GetAllBooks()
        {
            return _context.Books
                .Include(b => b.Author)
                .ToList();
        }


        public Book GetBook(int id)
        {
            var book = _context.Books.Include(b=>b.Author).FirstOrDefault(x => x.Id == id);
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
            var authorExists = _context.Authors.Any(a => a.Id == book.AuthorId);
            if (!authorExists)
            {
                throw new Exception("Указанный автор не существует");
            }

            book.Id = 0;

            _context.Books.Add(book);
            _context.SaveChanges();
            return book;

        }

        
        public Book UpdateBook(int id, Book updatedBook)
        {
            var existBook = _context.Books.FirstOrDefault(x => x.Id == id);

            if (existBook == null) 
            {
                throw new Exception("Книга не найдена");
            }

            if (string.IsNullOrWhiteSpace(updatedBook.Title)) 
            {
                throw new Exception("Название книги обязательно");
            }

            var authorExists = _context.Authors.Any(a => a.Id == updatedBook.AuthorId);
            if (!authorExists)
            {
                throw new Exception("Указанный автор не существует");
            }
            existBook.Title = updatedBook.Title;
            existBook.PublishedYear = updatedBook.PublishedYear;
            existBook.AuthorId = updatedBook.AuthorId;

            _context.SaveChanges();
            return existBook;
        }

       
        public void DeleteBook(int id)
        {
            var existBook = _context.Books.FirstOrDefault(x => x.Id == id);
            if (existBook == null)
            {
                throw new Exception("Указанной книги не существует");
            }
            _context.Books.Remove(existBook);
            _context.SaveChanges();
        }

        public List<Book> SelectBookByPublishedYear(int year)
        {
            try
            {
                return _context.Books
                    .Where(b => b.PublishedYear.Year > year)
                    .Include(b => b.Author)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось получить книги, опубликованные после {year} года. Ошибка: {ex.Message}");
            }
        }
    }
}

