using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Repositories;
using FavoriteColors.Domain.Services.Communication;
using FavoriteColors.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Controllers
{
    [Route("/api/persons")]
    public class PersonsController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        ///     Gets all persons.
        /// </summary>
        /// <returns>List of persons.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var persons = (await _personRepository.GetAllAsync())
                            .Select(person => person.AsDto());

            return persons;
        }

        /// <summary>
        ///     Gets personal information associated with the id.
        /// </summary>
        /// <param name="id">Id of the person.</param>
        /// <returns>A specific person.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PersonDto), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person is null)
            {
                return BadRequest();
            }

            return Ok(person.AsDto());
        }

        /// <summary>
        ///     Gets all persons associated with the color.
        /// </summary>
        /// <param name="color">The favorite color of the persons.</param>
        /// <returns>List of persons.</returns>
        [HttpGet("/color/{color}")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetByColorAsync(Color color)
        {
            var persons = await _personRepository.GetByColorAsync(color);

            if (persons is null)
            {
                return BadRequest();
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreatePersonAsync(CreatePersonDto personDto)
        {
            Person person = new()
            {
                Id = personDto.Id,
                LastName = personDto.LastName,
                Name = personDto.Name,
                ZipCode = personDto.ZipCode,
                City = personDto.City,
                Color = personDto.Color
            };

            await _personRepository.CreateAsync(person);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = person.Id }, person.AsDto());
        }
    }
}
