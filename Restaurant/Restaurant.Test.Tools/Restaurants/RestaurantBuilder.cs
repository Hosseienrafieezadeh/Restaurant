using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.persistence.EF.Restaurants;

namespace Restaurant.Test.Tools.Restaurants
{
    public class RestaurantBuilder
    {
        private readonly restaurant.Entitis.Restaurants.Restaurant _restaurant;

        public RestaurantBuilder()
        {
            _restaurant = new restaurant.Entitis.Restaurants.Restaurant()
            {
                Name =  "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };

        }

        public RestaurantBuilder WithName(string restaurantName)
        {
            _restaurant.Name= restaurantName;
            return this;
        }

        public restaurant.Entitis.Restaurants.Restaurant Bulid()
        {
            return _restaurant;
        }
    }
}
