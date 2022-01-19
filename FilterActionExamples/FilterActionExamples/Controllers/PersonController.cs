using FilterActionExamples.Filters.ActionFilters;
using FilterActionExamples.services;
using FilterActionExamples.services.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.Controllers
{
    [AsyncActionFilterAttributes]
    public class PersonController : Controller
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = personRepository.GetAll();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Person());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidationAttributes]
        public IActionResult Create(Person newperson)
        {

            personRepository.AddPerson(newperson);
            personRepository.save();

            return RedirectToAction(nameof(Index));
        }
    }
}
