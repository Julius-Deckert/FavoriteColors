using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();

        Task<Person> GetByIdAsync(int id);

        Task<ActionResult<IEnumerable<Person>>> GetByColorAsync(ColorEnum color);

        Task CreateAsync(Person person);
    }
}
