using SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SPA.Controllers
{
    public class ItemController : ApiController
    {
        // GET: api/Item
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Item/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Item
        public IHttpActionResult Post([FromBody]Item item)
        {
            ShoppingList shoppingList = ShoppingListController.ShoppingLists
                                              .Where(s => s.Id == item.ShopingListId).FirstOrDefault();

            if(shoppingList == null)
            {
                return NotFound();
            }

            item.Id = shoppingList.Items.Count == 0 ? 0 : shoppingList.Items.Max(i => i.Id) + 1;
            shoppingList.Items.Add(item);

            return Ok(shoppingList);


        }

        // PUT: api/Item/5
        public IHttpActionResult Put(int id, [FromBody]Item item)
        {
            ShoppingList shopping = ShoppingListController.ShoppingLists
                                    .Where(s => s.Id == item.ShopingListId)
                                    .FirstOrDefault();

            if(shopping == null)
            {
                return NotFound();
            }

            Item changedItem = shopping.Items.Where(i => i.Id == id).FirstOrDefault();

            changedItem.IsChecked = item.IsChecked;

            //ShoppingListController.ShoppingLists.

            return Ok(shopping);

        }


        // DELETE: api/Item/5
        public IHttpActionResult Delete(int id)
        {
            ShoppingList sl = ShoppingListController.ShoppingLists[0];

            var item = sl.Items.Where(i => i.Id == id).FirstOrDefault();

            if(item == null)
            {
                return NotFound();
            }

            sl.Items.Remove(item);

            return Ok(sl);
        }
    }
}
