
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI_ContactManagement.Services;
using webAPI_ContactManagement.Services.IReposittory;

namespace webAPI_ContactManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public ContactsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet("", Name = nameof(Index))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        public IActionResult Index() => Ok(_personRepository.getAllPeople());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPerson([FromBody] Person newPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newPerson.Id < 1)
            {
                return BadRequest("Id cannot be zero");
            }

            _personRepository.Add(newPerson);

            return CreatedAtAction(nameof(Index), newPerson);
        }


        [HttpDelete]
        [Route("{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePerson(int personId)
        {
            if (personId < 1)
            {
                return BadRequest("Invalid ID supplied");
            }

            try
            {
                _personRepository.Delete(personId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("/api/contacts/findByName/{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FindByName(string Name)
        {

            try
            {
                var people = _personRepository.FindByName(Name);
                return Ok(people);

            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

        }

    }
}
