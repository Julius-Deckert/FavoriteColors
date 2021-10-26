using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Domain.Services;

namespace FavoriteColors.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly List<Person> persons = new();

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await Task.FromResult(persons);
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = persons.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(person);
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(Color color)
        {
            return await _personRepository.GetByColorAsync(color);
        }

        public async Task CreateAsync(Person person)
        {
            persons.Add(person);
            await Task.CompletedTask;
        }
    }
}