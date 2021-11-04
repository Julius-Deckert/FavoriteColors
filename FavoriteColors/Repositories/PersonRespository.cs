using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FavoriteColors.Models;

namespace FavoriteColors.Repositories
{
    /// <summary>
    ///     Implementation of the repository class which is respnsible for persons storage operations.
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        /*
        * The name, lastname and city can contain multiple chars with whitespaces
        * to cover cases like double first and last names or special city names.
        */
        private readonly Regex _regex = new(@"[a-zA-Z\s]+, [a-zA-Z\s]+, [0-9]+, [a-zA-Z\s]+, [0-9]{1}", RegexOptions.IgnoreCase);
        private readonly List<Person> _persons = new();

        public PersonRepository()
        {
            var rows = ReadCsvFile(@"sample-input.csv", _regex);

            CreatePersonListFromFileData(rows);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await Task.FromResult(_persons);
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = _persons.SingleOrDefault(person => person.Id == id);
            return await Task.FromResult(person);
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(Color color)
        {
            var personsByColor = _persons.Where(person => person.Color == color.ToString()).ToList();
            return await Task.FromResult(personsByColor);
        }

        public async Task CreateAsync(Person person)
        {
            _persons.Add(person);
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
                _persons.Add(new Person(row));
            }
        }
    }
}
