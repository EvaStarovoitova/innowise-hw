using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ex4.Models;
using ex4.Data;
using ex4.Services;

namespace ex4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService) 
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetAllAuthor()
        {
            try
            {
                var autors = _authorService.GetAllAuthors();
                return Ok(autors);
            }
            catch (Exception ex)
            {
                return BadRequest("Авторы не найдены"); 
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            try
            {
                var author = _authorService.GetAuthorById(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor(Author author)
        {
            try
            {
                var createdAuthor = _authorService.CreateAuthor(author); 
                return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Author> UpdateAuthor(int id, Author updateAuthor) 
        {
            try
            {
                var existAuthor = _authorService.UpdateAuthor(id, updateAuthor);
                return Ok(existAuthor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id) 
        {
            try
            {
                _authorService.DeleteAuthor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("with-book-count")]
        public ActionResult<List<object>> GetAuthorsWithBookCount()
        {
            try
            {
                var authors = _authorService.GetAuthorWithBookCount();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search/{name}")]
        public ActionResult<List<Author>> FindAuthorsByName(string name)
        {
            try
            {
                var authors = _authorService.FindAuthorsByName(name);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}