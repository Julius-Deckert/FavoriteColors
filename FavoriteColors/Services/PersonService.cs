using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Domain.Services;
using FavoriteColors.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public Task<ActionResult<IEnumerable<Person>>> GetByColorAsync(Color color)
        {
            return _personRepository.GetByColorAsync(color);
        }

        public async Task<SavePersonResponse> CreateAsync(Person person)
        {
            try
            {
                await _personRepository.CreateAsync(person);

                return new SavePersonResponse(person);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SavePersonResponse($"An error occurred when saving the person: {ex.Message}");
            }
        }
    }
}