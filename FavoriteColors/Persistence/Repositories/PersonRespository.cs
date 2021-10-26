using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColors.Persistence.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await context.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await context.Persons.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Person>>> GetByColorAsync(Color color)
        {
            var persons = new List<Person>();

            foreach (Person person in context.Persons)
            {
                if (person.Color == color.ToString())
                {
                    persons.Add(person);
                }
            }

            return await Task.FromResult(persons);
        }

        public async Task CreateAsync(Person person)
        {
            await context.Persons.AddAsync(person);
        }
    }
}
