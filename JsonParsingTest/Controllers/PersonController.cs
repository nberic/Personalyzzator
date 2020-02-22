using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonParsingTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using JsonParsingTest.Services;

namespace JsonParsingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            _logger.LogInformation($"GET: All Persons");
            return Ok(_personService.GetAll());
        }

        [HttpGet("{personId}")]
        public ActionResult<Person> Get(Guid personId)
        {
            #nullable enable
            Person? requestedPerson = _personService.Get(personId);
            #nullable disable

            // also should have this separated
            if (requestedPerson == null)
            {
                _logger.LogInformation($"No such user: { personId }");
                return NotFound(personId);
            }
            return Ok(requestedPerson);
        }

        [HttpPost]
        public ActionResult<Person> Create([ FromBody ] Person person)
        {
            _logger.LogInformation($"POST: { person }");
            Person createdPerson = _personService.Add(person);
            return Ok(createdPerson);
        }

        [HttpPut("{personId}")]
        public ActionResult<Person> Update(Guid personId, [ FromBody ] Person person)
        {
            _logger.LogInformation($"PUT: { personId } - { person }");

            #nullable enable
            Person? updatedPerson = _personService.Update(personId, person);
            #nullable disable

            // also should have this separated
            if (updatedPerson == null)
            {
                _logger.LogInformation($"No such user: { personId }");
                return NotFound(personId);
            }
            return NoContent();
        }

        [HttpDelete("{personId}")]
        public ActionResult<Person> Delete(Guid personId)
        {
            _logger.LogInformation($"DELETE: { personId }");

            #nullable enable
            Person? deletedPerson = _personService.Delete(personId);
            #nullable disable

            if (deletedPerson == null)
            {
                NotFound(personId);
            }

            return Ok(deletedPerson);
        }
    }
}
