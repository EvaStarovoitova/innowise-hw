using ex4.Models;

namespace ex4.Data
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetByIdWithBooks(int id);
        List<object> GetAuthorWithBookCount();
        List<Author> FindAuthorsByName(string name);
        bool AuthorExists(int id);
    }
}
