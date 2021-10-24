using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;

namespace FavoriteColors.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();

        Task<Person> GetByIdAsync(int id);

        IEnumerable<Person> GetByColorAsync(string color);

        Task CreateAsync(Person person);
    }
}
