using System.Collections.Generic;
using System.Text.RegularExpressions;
using FavoriteColors.Persistence.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FavoriteColors
{

#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            // The name, lastname and city can contain multiple chars with whitespaces
            // to cover cases like double first and last names or special city names.
            // The zip code is restricted to 5 digits (german zip code) => this restiction can be removed to allow non german zip codes later on.
            Regex regex = new Regex(@"[a-zA-Z\s]+, [a-zA-Z\s]+, [0-9]{5}, [a-zA-Z\s]+, [0-9]{1}", RegexOptions.IgnoreCase);

            string[] csvLines = System.IO.File.ReadAllLines(@"sample-input.csv");

            var persons = new List<string>();

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

                row = Id + 1 + ", " + row;
                Id++;
                persons.Add(row);
            }

            persons.Insert(0, "Id, LastName, Name, ZipCode, City, Color");

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>())
            {
                context.Database.EnsureCreated();
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
#pragma warning restore CS1591
}