using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_EventManagement.Services;
using WebAPI_EventManagement.Services.IRepositiory;

namespace WebAPI_EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IEventRepository _eventcontext;
        public EventsController(IEventRepository eventRepository)
        {
            this._eventcontext = eventRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Event>))]
        public IActionResult GetAll() => Ok(_eventcontext.GetAll());


        [HttpGet("{EventId}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int EventId)
        {
            var eventById = _eventcontext.GetById(EventId);

            if(eventById == null)
            {
                return NotFound();
            }

            return Ok(eventById);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody]Event newevent)
        {
            if(newevent.Id < 1)
            {
                return BadRequest("Invalid Id.");
            }
            _eventcontext.Add(newevent);
            return CreatedAtAction(nameof(GetById), new { EventId = newevent.Id }, newevent);  //to implement this we need to name the Http attributes.
        }

        [HttpDelete]
        [Route("{DeleteId}")]  //  api/2 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int DeleteId)
        {
            try
            {
                _eventcontext.Delete(DeleteId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
