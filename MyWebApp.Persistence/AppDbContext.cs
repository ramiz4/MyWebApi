using Microsoft.EntityFrameworkCore;
using MyWebApp.Core.Entities;

namespace MyWebApp.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        
        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<ContactInfo> ContactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}