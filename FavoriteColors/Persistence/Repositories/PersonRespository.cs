using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColors.Persistence.Repositories
{
    public class PersonRespository : BaseRepository, IPersonRepository
    {
        public PersonRespository(AppDbContext context) : base(context)
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

        public async Task CreateAsync(Person person)
        {
            await context.Persons.AddAsync(person);
        }
    }
}
