using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Domain.Models;
using FavoriteColors.Domain.Services;
using FavoriteColors.Extensions;
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

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var persons = await personService.ListAsync();
            return persons;
        }
    }
}
