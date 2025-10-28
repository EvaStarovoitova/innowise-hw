using ex4.Models;
namespace ex4.Services
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
        Author GetAuthorById(int id);
        Author CreateAuthor(Author author);
        Author UpdateAuthor(int id, Author updateAuthor);
        void DeleteAuthor(int id);
        List<object> GetAuthorWithBookCount();
        List<Author> FindAuthorsByName(string name);

    }
}
