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
         * The zip code is restricted to 5 digits (german zip code) => this restiction can be removed to allow non german zip codes later on.
         */
        Regex regex = new Regex(@"[a-zA-Z\s]+, [a-zA-Z\s]+, [0-9]{5}, [a-zA-Z\s]+, [0-9]{1}", RegexOptions.IgnoreCase);

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

            var fileData = ReadCsvFile(@"sample-input.csv", regex);

            CreateEntitiesFromFileData(fileData, builder);
        }

        private string[] ReadCsvFile(string filePath, Regex regex)
        {
            var personsList = new List<string>();

            string[] csvLines = System.IO.File.ReadAllLines(filePath);

            int Id = 0;

            for (int i = 0; i < csvLines.Length; i++)
            {
                var row = csvLines[i];

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
                row = Id + 1 + "," + row;
                Id++;

                //remove whitespaces after comma
                row = Regex.Replace(row, " *, *", ",");

                personsList.Add(row);
            }

            return personsList.ToArray();
        }

        private void CreateEntitiesFromFileData(string[] fileData, ModelBuilder builder)
        {
            for (int i = 0; i < fileData.Length; i++)
            {
                builder.Entity<Person>().HasData
                (
                    new Person(fileData[i])
                );
            }
        }
    }
}
