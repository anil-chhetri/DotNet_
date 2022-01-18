using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_EventManagement.Services.IRepositiory
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Event GetById(int Id);

        Event Add(Event NewEvent);

        void Delete(int id);

    }
}
