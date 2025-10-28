using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ex4.Models;
using ex4.Data;
using ex4.Services;

namespace ex4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBook()
        {
            try
            {
                var books =_bookService.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            try
            {
                var book = _bookService.GetBook(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            try
            {
                var createdBook = _bookService.CreateBook(book);
                return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }      
        }

        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, Book updatedBook)
        {
            try
            {
                var existBook = _bookService.UpdateBook(id, updatedBook);
                return Ok(existBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-year/{year}")]
        public ActionResult<List<Book>> GetBooksByYear(int year)
        {
            try
            {
                var books = _bookService.SelectBookByPublishedYear(year);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
