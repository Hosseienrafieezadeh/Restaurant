using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Resturants.Contracts.Dtos;

namespace Restaurant.Test.Tools.Restaurants
{
    public static class AddresturantDtoFactory
    {
        public static AddRestaurantDto Create( string? name=null)
        {
            return new AddRestaurantDto()
            {
                Name = name??"ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
        }
    }
}
