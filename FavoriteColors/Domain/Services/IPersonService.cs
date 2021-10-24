﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services.Communication;

namespace FavoriteColors.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllAsync();

        Task<Person> GetByIdAsync(int id);

        IEnumerable<Person> GetByColorAsync(string color);

        Task<SavePersonResponse> CreateAsync(Person person);
    }
}
