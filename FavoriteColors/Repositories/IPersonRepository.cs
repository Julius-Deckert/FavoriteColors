using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Models;

namespace FavoriteColors.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetByColorAsync(Color color);
        Task CreateAsync(Person person);
    }
}
