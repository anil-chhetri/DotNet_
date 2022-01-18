using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataExamples.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataExamples.Controllers
{
    
    public class CustomersController : ODataController
    {
        private readonly ODataDbContext _context;

        public CustomersController(ODataDbContext context)
        {
            _context = context;
        }


        #region DemoData 
        private readonly List<string> demoCustomers = new List<string>
        {
            "Foo",
            "Bar",
            "Acme",
            "King of Tech",
            "Awesomeness"
        };

        private readonly List<string> demoProducts = new List<string>
        {
            "Bike",
            "Car",
            "Apple",
            "Spaceship"
        };

        private readonly List<string> demoCountries = new List<string>
        {
            "AT",
            "DE",
            "CH"
        };

        [HttpPost]
        [Route("fill")]
        public async Task<IActionResult> Fill()
        {
            var rand = new Random();
            for (var i = 0; i < 10; i++)
            {
                var c = new Customer
                {
                    CustomerName = demoCustomers[rand.Next(demoCustomers.Count)],
                    CountryId = demoCountries[rand.Next(demoCountries.Count)]
                };
                _context.Customer.Add(c);

                for (var j = 0; j < 10; j++)
                {
                    var o = new Order
                    {
                        OrderDate = DateTime.Today,
                        Product = demoProducts[rand.Next(demoProducts.Count)],
                        Quantity = rand.Next(1, 5),
                        Revenue = rand.Next(100, 5000),
                        Customer = c
                    };
                    _context.Orders.Add(o);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }


        #endregion


        //examples: 
        /* 
         * /odata/customers?$select=customerName&$expand=orders
         * /odata/customers?$filter=countryid eq 'CH' 
         *  /odata/customers?$select=customerName
         */

        [EnableQuery]
        public IActionResult Get() =>  Ok(_context.Customer);

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }


    }
}
