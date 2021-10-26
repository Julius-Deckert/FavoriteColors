using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;

namespace FavoriteColors.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllAsync();

        Task<Person> GetByIdAsync(int id);

        Task<IEnumerable<Person>> GetByColorAsync(Color color);

        Task CreateAsync(Person person);
    }
}
