using ex4.Data;
using ex4.Models;
using Microsoft.AspNetCore.Mvc;

namespace ex4.Services
{
    public class BookService
    {
        public List<Book> GetAllBooks()
        {
            try
            {
                return DataContext.Books;
            }
            catch (Exception ex)
            {
                throw new Exception("Книги не найдены");
            }
        }

       
        public Book GetBook(int id)
        {
            var book = DataContext.Books.FirstOrDefault(x => x.Id == id);
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
            var authorExists = DataContext.Authors.Any(a => a.Id == book.AuthorId);
            if (!authorExists)
            {
                throw new Exception("Указанный автор не существует");
            }

            var newId = DataContext.Books.Any() ? DataContext.Books.Max(a => a.Id) + 1 : 1;
            book.Id = newId;

            DataContext.Books.Add(book);
            return book;

        }

        
        public Book UpdateBook(int id, Book updatedBook)
        {
            var existBook = DataContext.Books.FirstOrDefault(x => x.Id == id);
      
            var authorExists = DataContext.Authors.Any(a => a.Id == updatedBook.AuthorId);
            if (!authorExists)
            {
                throw new Exception("Указанный автор не существует");
            }

            existBook.Title = updatedBook.Title;
            existBook.AuthorId = updatedBook.AuthorId;
            existBook.PublishedYear = updatedBook.PublishedYear;

            return existBook;
        }

       
        public void DeleteBook(int id)
        {
            var existBook = DataContext.Books.FirstOrDefault(x => x.Id == id);
            if (existBook == null)
            {
                throw new Exception("Указанной книги не существует");
            }
            DataContext.Books.Remove(existBook);
        }
    }
}

