using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_EventManagement.Services.IRepositiory;

namespace WebAPI_EventManagement.Services
{

    public record Event (int Id, DateTime date, string location, string descriptions);

    public class EventsRepository : IEventRepository
    {
        public List<Event> Events { get; } = new();

        public Event Add(Event NewEvent)
        {
            Events.Add(NewEvent);
            return NewEvent;
        }

        public void Delete(int id)
        {
            var fromdb = GetById(id);

            if(fromdb == null)
            {
                throw new ArgumentException($"No events exists with the given id : {id}", nameof(id));
            }

            Events.Remove(fromdb);
        }

        public IEnumerable<Event> GetAll() => Events;

        public Event GetById(int Id) => Events.FirstOrDefault(x => x.Id == Id);
    }
}
