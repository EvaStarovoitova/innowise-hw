namespace ex4.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Author(int id, string name, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}
