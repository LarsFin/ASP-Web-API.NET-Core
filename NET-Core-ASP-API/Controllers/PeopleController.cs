using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NETCoreASPAPI.Models;
using NETCoreASPAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETCoreASPAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPersonService personService;

        public PeopleController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return personService.GetPersons();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return null;
        }


    }
}
