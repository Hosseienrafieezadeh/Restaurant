using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Orders.Contracts.Dtos;

namespace Restaurant.Test.Tools.Orders
{
    public static class AddOrdersDtoFactory
    {
        public static AddOrdersDto Create(int UserId, int resaurantId, DateTime? date = null)
        {
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            return new AddOrdersDto
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = UserId,
                RestaurantId = resaurantId,
                TotalAmount = 4000
            };
        }
    }
}


