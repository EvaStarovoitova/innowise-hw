using ex4.Data;
using ex4.Models;

namespace ex4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRep;

        public AuthorService(IAuthorRepository authorRep)
        {
            _authorRep = authorRep;
        }

        public List<Author> GetAllAuthors()
        {
            return _authorRep.GetAll();
        }

        public Author GetAuthorById(int id)
        {
            var author = _authorRep.GetById(id);
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

            return _authorRep.Create(author);
        }

        public Author UpdateAuthor(int id, Author updateAuthor)
        {
            var existingAuthor = _authorRep.GetById(id);
            if (existingAuthor == null)
            {
                throw new Exception("Указанный автор не существует");
            }

            if (string.IsNullOrEmpty(updateAuthor.Name))
            {
                throw new Exception("Имя автора обязательно");
            }

            existingAuthor.Name = updateAuthor.Name;
            existingAuthor.DateOfBirth = updateAuthor.DateOfBirth;

            return _authorRep.Update(existingAuthor);
        }

        public void DeleteAuthor(int id)
        {
            var existAuthor = _authorRep.GetByIdWithBooks(id); 
            if (existAuthor == null)
            {
                throw new Exception("Указанный автор не существует");
            }

            _authorRep.Delete(id);
        }

        public List<object> GetAuthorWithBookCount()
        {
            try
            {
                return _authorRep.GetAuthorWithBookCount();
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
                return _authorRep.FindAuthorsByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при поиске авторов по имени '{name}': {ex.Message}");
            }
        }
    }
}