using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;

namespace FavoriteColors.Domain.Services
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> ListAsync();
    }
}
