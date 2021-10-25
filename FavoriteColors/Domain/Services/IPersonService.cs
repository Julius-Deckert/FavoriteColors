using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllAsync();

        Task<Person> GetByIdAsync(int id);

        Task<ActionResult<IEnumerable<Person>>> GetByColorAsync(ColorEnum color);

        Task<SavePersonResponse> CreateAsync(Person person);
    }
}
