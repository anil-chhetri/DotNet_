using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Models;

namespace Catalog.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly List<Item> Items = new()
        {
            new Item { Id = Guid.NewGuid(), Name="Potion", Price=9, CreatedDate=DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name="Iron Sword", Price=20 , CreatedDate=DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name="Bronze Shield", Price=19, CreatedDate=DateTimeOffset.UtcNow },
        };

        public IEnumerable<Item> GetAll() => Items;

        public Item GetById(Guid Id) => Items.FirstOrDefault(x => x.Id == Id); 


        public Item GetByName(string name) => Items.FirstOrDefault(x => x.Name == name);

        public Item AddItem(Item newItem) 
        {
           
            Items.Add(newItem);
            return newItem;
        }


        public bool Save() => true;

        public Item UpdateItem(Item item)
        {
           var index = Items.FindIndex(x => x.Id == item.Id);
           Items[index] = item;
           return item;
        }

        public void Delete(Item item)
        {
            var index = Items.FindIndex(x => x.Id == item.Id);
            Items.RemoveAt(index);
        }
    }
}