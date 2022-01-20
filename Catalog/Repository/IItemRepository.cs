using System;
using System.Collections.Generic;
using Catalog.Models;

namespace Catalog.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();

        Item GetById(Guid Id);

        Item GetByName(string name);

        Item AddItem(Item newItem);

        Item UpdateItem(Item item);

        void Delete(Item item);

        bool Save();
        
    }
}