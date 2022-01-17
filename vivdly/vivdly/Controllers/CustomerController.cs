using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vivdly.Models;
using vivdly.ViewModels;

namespace vivdly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer() { ID = 1, Name = "John Smith" },
                new Customer() { ID = 2, Name = "Mary Williams" },

            };

        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerViewModel()
            {
                Customers = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerViewModel customer)
        {
            if(!ModelState.IsValid)
            {
                customer.MembershipTypes = _context.MembershipTypes.ToList();
                return View("CustomerForm", customer);
            }
            if(customer.Customers.ID == 0)
            {
                _context.Customers.Add(customer.Customers);

            }
            else
            {
                var dbCustomer = _context.Customers.Single(c => c.ID == customer.Customers.ID);

                dbCustomer.Name = customer.Customers.Name;
                dbCustomer.DateOfBirth = customer.Customers.DateOfBirth;
                dbCustomer.MembershipTypeID = customer.Customers.MembershipTypeID;
                dbCustomer.IsSubscribedToNewsLetter = customer.Customers.IsSubscribedToNewsLetter;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.Single(x => x.ID == id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Single(c => c.ID == id);
            var customerViewModel = new CustomerViewModel
            {
                Customers = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", customerViewModel);
        }
        
        // GET: Customer
        [Route("customers")]
        public ActionResult Index()
        {
            // here include refers to egar loading.
            //var customer = _context.Customers.Include(c => c.MembershipType).ToList();
            return View();  //using ajax to get mvoies
        }

        [Route("customers/{id}")]
        public ActionResult Details(int? id)
        {
            //Customer cust = GetCustomers().Where(x => x.ID == id).FirstOrDefault();
            Customer cust = _context.Customers.Include(c=> c.MembershipType).SingleOrDefault(x => x.ID == id);
            if (cust == null)
            {
                return HttpNotFound();
            }
            return View(cust);
        }
    }
}