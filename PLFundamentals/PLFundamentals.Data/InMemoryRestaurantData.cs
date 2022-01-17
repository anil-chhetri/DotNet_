using PLFundamentals.Core;
using System.Collections.Generic;
using System.Linq;

namespace PLFundamentals.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {

        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{ Id=1, Cuisines=Cuisines.Italian, Name="Scott's Pizza", Location="MaryLand"},
                new Restaurant{ Id=2, Cuisines=Cuisines.None, Name="Cinnamon Club", Location="London"},
                new Restaurant{ Id=3, Cuisines=Cuisines.Mexician, Name="La Costa", Location="Carlifornia"}
            };
        }


        public Restaurant GetRestaurantById(int restuarantID)
        {
            return restaurants.SingleOrDefault(x => x.Id == restuarantID);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string ResName)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(ResName) || r.Name.Contains(ResName)
                   orderby r.Name
                   select r;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            return restaurant;
        }

       
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);

            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisines = updatedRestaurant.Cuisines;
            }
            return restaurant;
        }

        public int commit()
        {
            return 0;
        }

        Restaurant IRestaurantData.Delete(int id)
        {
            var restaurant = restaurants.Where(r => r.Id == id).SingleOrDefault();
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        int IRestaurantData.RestaurantCount()
        {
            return restaurants.Count;
        }
    }
}
