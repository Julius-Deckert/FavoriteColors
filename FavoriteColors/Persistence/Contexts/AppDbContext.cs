using System.Collections.Generic;
using System.Text.RegularExpressions;
using FavoriteColors.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColors.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        /*
         * The name, lastname and city can contain multiple chars with whitespaces
         * to cover cases like double first and last names or special city names.
         * The zip code is restricted to 5 digits (german zip code) => this restriction can be removed to allow non german zip codes later on.
         */
        private readonly Regex _regex = new Regex(@"[a-zA-Z\s]+, [a-zA-Z\s]+, [0-9]{5}, [a-zA-Z\s]+, [0-9]{1}", RegexOptions.IgnoreCase);

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

            var fileData = ReadCsvFile(@"sample-input.csv", _regex);

            CreateEntitiesFromFileData(fileData, builder);
        }

        private static IEnumerable<string> ReadCsvFile(string filePath, Regex regex)
        {
            var personsList = new List<string>();

            var csvLines = System.IO.File.ReadAllLines(filePath);

            var id = 0;

            foreach (var line in csvLines)
            {
                var row = line;

                // Add comma between zip code and city to match person properties
                row = Regex.Replace(row, "[0-9]{5}", "$0,");

                /* 
                 * If the row does not match the regex its invalid
                 * and therefore not considered for further processing.
                 */
                if (!regex.IsMatch(row))
                {
                    continue;
                }

                //add corresponding id to person
                row = id + 1 + "," + row;
                id++;

                //remove whitespaces after comma
                row = Regex.Replace(row, " *, *", ",");

                personsList.Add(row);
            }

            return personsList.ToArray();
        }

        private static void CreateEntitiesFromFileData(IEnumerable<string> fileData, ModelBuilder builder)
        {
            foreach (var row in fileData)
            {
                builder.Entity<Person>().HasData
                (
                    new Person(row)
                );
            }
        }
    }
}
