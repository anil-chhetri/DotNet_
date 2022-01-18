using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI_ContactManagement.Services.IReposittory
{
    public interface IPersonRepository
    {
        IEnumerable<Person> getAllPeople();

        Person Add(Person newPerson);

        void Delete(int personId);

        IEnumerable<Person> FindByName(string name);


    }
}
