using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Domain.Services;
using FavoriteColors.Domain.Services.Communication;

namespace FavoriteColors.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await personRepository.GetAllAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await personRepository.GetByIdAsync(id);
        }

        public async Task<SavePersonResponse> CreateAsync(Person person)
        {
            try
            {
                await personRepository.CreateAsync(person);

                return new SavePersonResponse(person);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SavePersonResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }
    }
}