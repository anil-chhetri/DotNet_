using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.services.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext context;

        public PersonRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Person AddPerson(Person newPerson)
        {
            context.Add(newPerson);
            return newPerson;
        }

        public IEnumerable<Person> GetAll() => context.People;

        public void save() => context.SaveChanges();
    }
}
