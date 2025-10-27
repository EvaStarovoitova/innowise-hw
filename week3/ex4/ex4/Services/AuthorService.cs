using ex4.Data;
using ex4.Models;
using Microsoft.AspNetCore.Mvc;

namespace ex4.Services
{
    public class AuthorService
    {
        public List<Author> GetAllAuthors()
        {
            return DataContext.Authors;
        }

       
        public Author GetAuthorById(int id)
        {
            var author = DataContext.Authors.FirstOrDefault(x => x.Id == id);
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

            var newId = DataContext.Authors.Any() ? DataContext.Authors.Max(x => x.Id) + 1 : 1;
            author.Id = newId;

            DataContext.Authors.Add(author);

            return author;
        }

        
        public Author UpdateAuthor(int id, Author updateAuthor)
        {
            var existAuthor = DataContext.Authors.FirstOrDefault(x => x.Id == id);
            if (existAuthor == null)
            {
                throw new Exception("Указанный автор не существует");
            }

            if (string.IsNullOrEmpty(updateAuthor.Name))
            {
                throw new Exception("Имя автора обязательно");
            }

            existAuthor.Name = updateAuthor.Name;
            existAuthor.DateOfBirth = updateAuthor.DateOfBirth;

            return existAuthor;
        }

        
        public void DeleteAuthor(int id)
        {
            var existAuthor = DataContext.Authors.FirstOrDefault(x => x.Id == id);
            if (existAuthor == null)
            {
                throw new Exception("Указанный автор не существует");
            }

            DataContext.Authors.Remove(existAuthor);
        }
    }
}
