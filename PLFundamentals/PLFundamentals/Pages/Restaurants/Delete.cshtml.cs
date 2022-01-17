using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PLFundamentals.Core;
using PLFundamentals.Data;

namespace PLFundamentals.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public DeleteModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public Restaurant Restaurant { get; set; }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetRestaurantById(restaurantId);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();

        }

        public IActionResult OnPost(int restaurantId)
        {
            Restaurant = restaurantData.Delete(restaurantId);
            restaurantData.commit();

            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{Restaurant.Name} is Deleted.";

            return RedirectToPage("List");
        }
    }
}
