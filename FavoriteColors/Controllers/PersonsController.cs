using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services;
using FavoriteColors.Extensions;
using FavoriteColors.Resources;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Controllers
{
    [Route("/api/persons")]
    public class PersonsController : Controller
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        public PersonsController(IPersonService personService, IMapper mapper)
        {
            this.personService = personService;
            this.mapper = mapper;
        }

        /// <summary>
        ///     Lists all persons.
        /// </summary>
        /// <returns>List os categories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var persons = await personService.ListAsync();
            return persons;
        }

        /// <summary>
        ///     Saves a new person.
        /// </summary>
        /// <param name="resource">Data of new person.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Person), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SavePersonResource resource)
        {
            var person = mapper.Map<SavePersonResource, Person>(resource);
            var result = await personService.SaveAsync(person);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result);
        }
    }
}
