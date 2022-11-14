using Microsoft.EntityFrameworkCore;
using MaintainBookApi.Entities;


namespace MaintainBookApi.DbContexts
{
    public class BookInfoContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;

        public BookInfoContext(DbContextOptions<BookInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasData(
                new Book("PS", "Kalki")
                {
                    Id = 1
                },
                new Book("PS2", "Kalki2")
                {
                    Id = 2
                },
                new Book("PS3", "Kalki3")
                {
                    Id = 3
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
