using SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPA.Controllers
{
    public class ShoppingListController : ApiController
    {

        public static List<ShoppingList> ShoppingLists = new List<ShoppingList>()
        {
            new ShoppingList () { Id = 1, Name = "Groceries", Items = { 
                new Item() { Id = 1,  Name = "Milk", ShopingListId=1},
                new Item() { Id = 2,  Name = "CornFlakes", ShopingListId=1}
                } },
            new ShoppingList () { Id = 2, Name = "Hardware"},
            new ShoppingList () { Id = 3, Name = "Restaurant"},
        };
        // GET: api/ShoppingList
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ShoppingList/5
        public IHttpActionResult Get(int id)
        {
            ShoppingList list = ShoppingLists.FirstOrDefault(l => l.Id == id);
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // POST: api/ShoppingList
        public IEnumerable<ShoppingList> Post([FromBody]ShoppingList shoppingList)
        {
            shoppingList.Id = ShoppingLists.Count;
            ShoppingLists.Add(shoppingList);
            return ShoppingLists;
        }

    }
}
