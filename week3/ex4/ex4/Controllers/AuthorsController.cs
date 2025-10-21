using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ex4.Models;
using ex4.Data;

namespace ex4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Author>> GetAllAuthor()
        {
            try
            {
                return Ok(DataContext.Authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = DataContext.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return NotFound("Указанный автор не существует");
            }
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor(Author author)
        {
            if (string.IsNullOrEmpty(author.Name))
            {
                return BadRequest("Имя автора обязательно");
            }

            var newId = DataContext.Authors.Any() ? DataContext.Authors.Max(x => x.Id) + 1 : 1;
            author.Id = newId;

            DataContext.Authors.Add(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public ActionResult<Author> UpdateAuthor(int id, Author updateAuthor) 
        {
            var existAuthor = DataContext.Authors.FirstOrDefault(x => x.Id == id);
            if (existAuthor == null)
            {
                return NotFound("Указанный автор не существует"); 
            }

            if (string.IsNullOrEmpty(updateAuthor.Name))
            {
                return BadRequest("Имя автора обязательно");
            }

            existAuthor.Name = updateAuthor.Name;
            existAuthor.DateOfBirth = updateAuthor.DateOfBirth;

            return Ok(existAuthor);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id) 
        {
            var existAuthor = DataContext.Authors.FirstOrDefault(x => x.Id == id);
            if (existAuthor == null)
            {
                return NotFound("Указанный автор не существует"); 
            }

            DataContext.Authors.Remove(existAuthor);
            return NoContent();
        }
    }
}