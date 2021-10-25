using System.Collections.Generic;
using System.Text.RegularExpressions;
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

            //builder.Entity<Person>().HasData
            //(
            //    new Person
            //    {
            //        Id = 1,
            //        Name = "Max",
            //        LastName = "Mustermann",
            //        ZipCode = 12345,
            //        City = "Musterstadt",
            //        Color = "blau"
            //    }
            //);

            //builder.Entity<Person>().HasData
            //(
            //    new Person
            //    {
            //        Id = 2,
            //        Name = "Erika",
            //        LastName = "Musterfrau",
            //        ZipCode = 67890,
            //        City = "Musterstadt",
            //        Color = "rot"
            //    }
            //);

            // The name, lastname and city can contain multiple chars with whitespaces
            // to cover cases like double first and last names or special city names.
            // The zip code is restricted to 5 digits (german zip code) => this restiction can be removed to allow non german zip codes later on.
            Regex regex = new Regex(@"[a-zA-Z\s]+, [a-zA-Z\s]+, [0-9]{5}, [a-zA-Z\s]+, [0-9]{1}", RegexOptions.IgnoreCase);

            string[] csvLines = System.IO.File.ReadAllLines(@"sample-input.csv");

            var personsList = new List<string>();

            int Id = 0;

            for (int i = 0; i < csvLines.Length; i++)
            {
                var row = csvLines[i];

                // Add comma between zip code and city to match person properties
                row = Regex.Replace(row, "[0-9]{5}", "$0,");

                if (!regex.IsMatch(row))
                {
                    continue;
                }

                //add corresponding id to person
                row = Id + 1 + "," + row;
                Id++;

                //remove whitespaces after comma
                row = Regex.Replace(row, " *, *", ",");

                personsList.Add(row);
            }

            var personsArray = personsList.ToArray();

            for (int i = 0; i < personsArray.Length; i++)
            {
                builder.Entity<Person>().HasData
                (
                    new Person(personsArray[i])
                );
            }
        }
    }
}
