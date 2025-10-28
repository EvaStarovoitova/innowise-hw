using System.Text.Json.Serialization;

namespace ex4.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public Book() { }
        public Book(string title, DateTime publishedYear, int authorId)
        {
            Title = title;
            PublishedYear = publishedYear;
            AuthorId = authorId;
        }
    }
}
