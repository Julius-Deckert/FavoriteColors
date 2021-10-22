using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Domain.Services;

namespace FavoriteColors.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            return await personRepository.ListAsync();
        }

        public async Task<SavePersonResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }
    }
}