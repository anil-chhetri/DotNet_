using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vivdly.Models;

namespace vivdly.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public Customer Customers { get; set; }
    }
}