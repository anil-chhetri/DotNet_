using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI_ContactManagement.Services.IReposittory;

namespace webAPI_ContactManagement.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> Peoples; 

        public PersonRepository()
        {
            Peoples = new();
        }

        public Person Add(Person newPerson)
        {
            Peoples.Add(newPerson);
            return newPerson;
        }

        public void Delete(int personId)
        {
            var fromdb = Peoples.FirstOrDefault(p => p.Id == personId);
            
            if(fromdb == null)
            {
                throw new ArgumentException("Person not found", nameof(personId));
            }

            Peoples.Remove(fromdb);
        }

        public IEnumerable<Person> FindByName(string name)
        {
            var fromdb = Peoples.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).ToList();

            if(fromdb == null)
            {
                throw new ArgumentException("Invalid or missing name", nameof(name));
            }

            return fromdb;
        }

        public IEnumerable<Person> getAllPeople() => Peoples.ToList();
        
    }
}
