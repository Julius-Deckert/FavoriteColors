using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services;
using FavoriteColors.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Controllers
{
    [Route("/api/persons")]
    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        ///     Gets all persons.
        /// </summary>
        /// <returns>List of persons.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var persons = await _personService.GetAllAsync();
            return persons;
        }

        /// <summary>
        ///     Gets personal information associated with the id.
        /// </summary>
        /// <param name="id">Id of the person.</param>
        /// <returns>A specific person.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(Person), 404)]
        public async Task<ActionResult<Person>> GetByIdAsync(int id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        ///     Gets all persons associated with the color.
        /// </summary>
        /// <param name="color">The favorite color of the persons.</param>
        /// <returns>List of persons.</returns>
        [HttpGet("/color/{color}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(Person), 404)]
        public async Task<ActionResult<IEnumerable<Person>>> GetByColorAsync(Color color)
        {
            var persons = await _personService.GetByColorAsync(color);

            if (persons is null)
            {
                return NotFound();
            }

            return Ok(persons);
        }

        /// <summary>
        ///     Create a new person.
        /// </summary>
        /// <param name="person">Data of new person.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Person), 201)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> PostAsync(Person person)
        {
            var result = await _personService.CreateAsync(person);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
