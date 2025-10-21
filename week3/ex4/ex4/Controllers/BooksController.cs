using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ex4.Models;
using ex4.Data;

namespace ex4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAllBook()
        {
            try
            {
                return Ok(DataContext.Books);
            }
            catch (Exception ex)
            {
                return NotFound("Книги не найдены");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = DataContext.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound("Книга не найдена");
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return BadRequest("Название книги обязательно");
            }
            var authorExists = DataContext.Authors.Any(a => a.Id == book.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Указанный автор не существует");
            }

            var newId = DataContext.Books.Any() ? DataContext.Books.Max(a => a.Id) + 1 : 1;
            book.Id = newId;

            DataContext.Books.Add(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);

        }

        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, Book updatedBook)
        {
            var existBook=DataContext.Books.FirstOrDefault(x => x.Id == id);
            if (existBook == null)
            {
                return NotFound();
            }

            var authorExists = DataContext.Authors.Any(a => a.Id == updatedBook.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Указанный автор не существует");
            }

            existBook.Title = updatedBook.Title;
            existBook.AuthorId = updatedBook.AuthorId;
            existBook.PublishedYear = updatedBook.PublishedYear;

            return Ok(existBook);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var existBook=DataContext.Books.FirstOrDefault(x => x.Id == id);
            if (existBook == null)
            {
                return NotFound();
            }

            DataContext.Books.Remove(existBook);
            return NoContent();
        }
    }
}
