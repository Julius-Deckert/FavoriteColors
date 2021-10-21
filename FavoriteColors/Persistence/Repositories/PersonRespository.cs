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

        public async Task<IEnumerable<Person>> ListAsync()
        {
            return await context.Persons.ToListAsync();
        }
    }
}
