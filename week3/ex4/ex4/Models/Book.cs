namespace ex4.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int AuthorId { get; set; }


        public Book(int id, string title, DateTime publishedYear, int authorId)
        {
            Id = id;
            Title = title;
            PublishedYear = publishedYear;
            AuthorId = authorId;
        }

    }
}
