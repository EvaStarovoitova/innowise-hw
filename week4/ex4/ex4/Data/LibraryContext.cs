using Microsoft.EntityFrameworkCore;
using ex4.Models;

namespace ex4.Data
{
    public class LibraryContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Ex4.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b=>b.Id);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(50);
                entity.Property(b=>b.PublishedYear).IsRequired();

            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a=>a.Name).IsRequired().HasMaxLength(50);
                entity.Property(a=>a.DateOfBirth).IsRequired();
            });

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "автор1", DateOfBirth = new DateTime(1888, 8, 8) },
                new Author { Id = 2, Name = "автор2", DateOfBirth = new DateTime(1999, 9, 9) },
                new Author { Id = 3, Name = "фвтор3", DateOfBirth = new DateTime(1777, 7, 7) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "книга1", PublishedYear = new DateTime(2014, 1, 1), AuthorId = 1 },
                new Book { Id = 2, Title = "книга2", PublishedYear = new DateTime(1920, 1, 1), AuthorId = 1 },
                new Book { Id = 3, Title = "книга3", PublishedYear = new DateTime(1799, 1, 1), AuthorId = 2 },
                new Book { Id = 4, Title = "книга4", PublishedYear = new DateTime(2016, 1, 1), AuthorId = 3 }
            );
        }

    }
}
