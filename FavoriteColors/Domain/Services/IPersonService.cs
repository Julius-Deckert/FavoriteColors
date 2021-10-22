using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services.Communication;

namespace FavoriteColors.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> ListAsync();
        Task<SavePersonResponse> SaveAsync(Person category);
    }
}
