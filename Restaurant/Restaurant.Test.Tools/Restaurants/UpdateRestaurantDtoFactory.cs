using restaurants.Services.Resturants.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Test.Tools.Restaurants
{
    public static class UpdateRestaurantDtoFactory
    {
        public static UpdateRestaurantDto Create(string? name = null)
        {
            return new UpdateRestaurantDto()
            {
                Name = name ?? "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
        }
    }
}
