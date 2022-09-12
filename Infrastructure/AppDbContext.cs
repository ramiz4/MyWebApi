#pragma warning disable CS1591
#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<ContactInfo> ContactInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}