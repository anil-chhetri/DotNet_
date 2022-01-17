using Microsoft.EntityFrameworkCore;
using PLFundamentals.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLFundamentals.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly DatabaseDbContext context;

        public SqlRestaurantData(DatabaseDbContext context)
        {
            this.context = context;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);
            return restaurant;
        }

        public int commit()
        {
            return context.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = context.Restaurants.Where(r => r.Id == id).SingleOrDefault();
            context.Restaurants.Remove(restaurant);
            return restaurant;
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = context.Restaurants.Where(r => r.Id == restaurantId).SingleOrDefault();
            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string ResName)
        {
            var restaurant = context.Restaurants
                            .Where(r => string.IsNullOrEmpty(ResName) || r.Name.StartsWith(ResName) )  
                            .OrderBy(r => r.Name)
                            .ToList();

            return restaurant;
                            
        }

        public int RestaurantCount()
        {
            return context.Restaurants.Count();
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = context.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
