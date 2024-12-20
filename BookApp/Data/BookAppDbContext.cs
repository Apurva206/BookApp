using BookApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookApp.Data
{
    
        public class BookAppDbContext : DbContext
        {
            public BookAppDbContext(DbContextOptions<BookAppDbContext> options) : base(options) { }

            public DbSet<Book> Books { get; set; }
            public DbSet<User> User { get; set; }
        }
}
