using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FavoriteColors.Models;
using Microsoft.Extensions.Logging;

namespace FavoriteColors.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        /*
        * The name, lastname and city can contain multiple chars with whitespaces
        * to cover cases like double first and last names or special city names.
        */
        private readonly Regex _regex = new(@"[a-zA-Z\s]+, [a-zA-Z\s]+, [0-9]+, [a-zA-Z\s]+, [0-9]{1}", RegexOptions.IgnoreCase);
        private readonly List<Person> persons = new();
        private readonly ILogger<PersonRepository> logger;

        public PersonRepository(ILogger<PersonRepository> logger)
        {
            var rows = ReadCsvFile(@"sample-input.csv", _regex);

            this.logger = logger;

            CreatePersonListFromFileData(rows);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await Task.FromResult(persons);
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var item = persons.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(Color color)
        {
            var personsByColor = persons.Where(item => item.Color == color.ToString()).ToList();
            return await Task.FromResult(personsByColor);
        }

        public async Task CreateAsync(Person person)
        {
            persons.Add(person);
            await Task.CompletedTask;
        }

        private IEnumerable<string> ReadCsvFile(string filePath, Regex regex)
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
                    logger.LogInformation($"The personal information '{row}' does not match the given convensions. " +
                        $"Therefore this personal information is classifed as invalid and isn't considered for further processing.");
                    continue;
                }

                //add corresponding id to person
                row = id + 1 + "," + row;
                id++;

                //remove whitespaces after comma
                row = Regex.Replace(row, " *, *", ",");

                personsList.Add(row);
            }

            return personsList;
        }

        private void CreatePersonListFromFileData(IEnumerable<string> fileData)
        {
            foreach (var row in fileData)
            {
                persons.Add(new Person(row));
            }
        }
    }
}
