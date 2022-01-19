using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.services.Repository
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();

        Person AddPerson(Person newPerson);

        void save();

    }
}
