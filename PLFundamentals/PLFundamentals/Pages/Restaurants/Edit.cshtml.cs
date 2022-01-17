using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PLFundamentals.Core;
using PLFundamentals.Data;

namespace PLFundamentals.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> CuisinesItems { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            CuisinesItems = htmlHelper.GetEnumSelectList<Cuisines>();

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }


        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                CuisinesItems = htmlHelper.GetEnumSelectList<Cuisines>();
                return Page();
            }
            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
            }
            else
            {
                restaurantData.Add(Restaurant);

            }
            restaurantData.commit();
            TempData["Message"] = "Data saved successfully.";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
