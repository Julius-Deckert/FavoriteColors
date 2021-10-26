using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColors.Persistence.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        private List<Person> persons = new();

        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public PersonRepository()
        {
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await Context.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await Context.Persons.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(Color color)
        {
            var persons = Context.Persons
                .Where(person => person.Color == color.ToString()).ToList();

            return await Task.FromResult(persons);
        }

        public async Task CreateAsync(Person person)
        {
            await Context.Persons.AddAsync(person);
        }
    }
}
