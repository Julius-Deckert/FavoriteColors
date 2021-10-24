﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services;
using FavoriteColors.Resources;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColors.Controllers
{
    [Route("/api/persons")]
    public class PersonsController : Controller
    {
        private readonly IPersonService personService;

        public PersonsController(IPersonService personService)
        {
            this.personService = personService;
        }

        /// <summary>
        ///     Gets all persons.
        /// </summary>
        /// <returns>List os categories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var persons = await personService.GetAllAsync();
            return persons;
        }

        /// <summary>
        ///     Gets personal information accociated with the id.
        /// </summary>
        /// <param name="id">Id of the person.</param>
        /// <returns>List os categories.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), 200)]
        public async Task<Person> GetByIdAsync(int id)
        {
            return await personService.GetByIdAsync(id);
        }

        /// <summary>
        ///     Gets all persons accociated with the color.
        /// </summary>
        /// <param name="id">Id of the person.</param>
        /// <returns>List os categories.</returns>
        [HttpGet("/color/{color}")]
        [ProducesResponseType(typeof(Person), 200)]
        public IEnumerable<Person> GetByColor(string color)
        {
            return personService.GetByColorAsync(color);
        }

        /// <summary>
        ///     Create a new person.
        /// </summary>
        /// <param name="resource">Data of new person.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Person), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] Person resource)
        {
            var result = await personService.CreateAsync(resource);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result);
        }
    }
}
