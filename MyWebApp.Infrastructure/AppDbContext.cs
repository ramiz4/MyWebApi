using Microsoft.EntityFrameworkCore;
using MyWebApp.Core.Models;

namespace MyWebApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Person>? Persons { get; set; }

        public virtual DbSet<ContactInfo>? ContactInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}