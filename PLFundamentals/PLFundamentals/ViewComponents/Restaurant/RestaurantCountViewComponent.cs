using Microsoft.AspNetCore.Mvc;
using PLFundamentals.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLFundamentals.ViewComponents.Restaurant
{
    public class RestaurantCountViewComponent
        : ViewComponent
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = this.restaurantData.RestaurantCount();
            return View(count);
        }
        
    }
}
