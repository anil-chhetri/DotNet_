using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.Models
{
    public class Item
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public bool IsChecked { get; set; } = false;

        public int ShopingListId { get; set; } = -1;


    }
}