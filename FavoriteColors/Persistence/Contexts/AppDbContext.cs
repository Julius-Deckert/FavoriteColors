using FavoriteColors.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColors.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<Person>().HasKey(p => p.Id);
            builder.Entity<Person>().Property(p => p.Id).IsRequired();
            builder.Entity<Person>().Property(p => p.Name).IsRequired();
            builder.Entity<Person>().Property(p => p.LastName).IsRequired();
            builder.Entity<Person>().Property(p => p.ZipCode).IsRequired();
            builder.Entity<Person>().Property(p => p.City).IsRequired();
            builder.Entity<Person>().Property(p => p.Color).IsRequired();

            builder.Entity<Person>().HasData
            (
                new Person {
                    Id = 1,
                    Name = "Max",
                    LastName = "Mustermann",
                    ZipCode = 12345,
                    City = "Musterstadt",
                    Color = "blau"
                }
            );

            builder.Entity<Person>().HasData
            (
                new Person
                {
                    Id = 2,
                    Name = "Erika",
                    LastName = "Musterfrau",
                    ZipCode = 67890,
                    City = "Musterstadt",
                    Color = "rot"
                }
            );
        }
    }
}
