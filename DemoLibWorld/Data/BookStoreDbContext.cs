using DemoLibWorld.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoLibWorld.Data
{
    public class BookStoreDbContext:DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {
                
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<DemoLibWorld.Model.BookCategory> BookCategory { get; set; }
    }
}
