using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BokSjelf.Models
{
    public class BokSjelfContext: DbContext
    {
        public BokSjelfContext(DbContextOptions<BokSjelfContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}