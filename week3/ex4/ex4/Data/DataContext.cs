using ex4.Models;

namespace ex4.Data
{
    public class DataContext
    {
        private static List<Book> _books;
        private static List<Author> _authors;

        public static List<Book> Books
        {
            get
            {
                _books ??= new List<Book>
                {
                    new Book (1, "Harry Potter", new DateTime(2001, 1, 1), 1 )
                };
                return _books;
            }
        }

        public static List<Author> Authors
        {
            get
            {
                _authors ??= new List<Author>
                {
                    new Author (1, "J.K. Rowling", new DateTime(1965,7,31))
                };
                return _authors;
            }
        }

    }
}
