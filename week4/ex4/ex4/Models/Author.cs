namespace ex4.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
        public ICollection<Book> Books { get; set; }   
        public Author(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}
