using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.Models
{
    public class ShoppingList
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public List<Item> Items { get; set; } = new List<Item>();

    }
}