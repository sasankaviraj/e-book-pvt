using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using e_book_pvt.Models;

namespace e_book_pvt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<e_book_pvt.Models.Book>? Book { get; set; }
        public DbSet<e_book_pvt.Models.Cart>? Cart { get; set; }
        public DbSet<e_book_pvt.Models.OrderDetail>? Order { get; set; }
    }
}
