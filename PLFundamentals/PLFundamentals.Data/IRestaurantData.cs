using PLFundamentals.Core;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLFundamentals.Data
{
    public interface IRestaurantData
    {
        public IEnumerable<Restaurant> GetRestaurantsByName(string ResName);
        public Restaurant GetRestaurantById(int restaurantId);

        public Restaurant Update(Restaurant updatedRestaurant);

        public Restaurant Add(Restaurant restaurant);

        public Restaurant Delete(int id);

        public int commit();

        int RestaurantCount();

    }
}
