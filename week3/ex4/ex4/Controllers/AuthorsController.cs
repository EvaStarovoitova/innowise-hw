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
        private readonly AuthorService _authorServices;

        public AuthorsController()
        {
            _authorServices = new AuthorService();
        }
           

        [HttpGet]
        public ActionResult<List<Author>> GetAllAuthor()
        {
            try
            {
                var autors = _authorServices.GetAllAuthors();
                return Ok(autors);
            }
            catch (Exception ex)
            {
                return NotFound("Авторы не найдены");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            try
            {
                var author = _authorServices.GetAuthorById(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor(Author author)
        {
            try
            {
                var createdAuthor = _authorServices.CreateAuthor(author); 
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
                var existAuthor = _authorServices.UpdateAuthor(id, updateAuthor);
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
                _authorServices.DeleteAuthor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}