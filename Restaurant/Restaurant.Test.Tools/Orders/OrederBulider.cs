using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurant.Entitis.Orders;

namespace Restaurant.Test.Tools.Orders
{
    public  class OrederBulider
    {
        private  readonly Order _orders;

        public OrederBulider(int UserId, int resaurantId)
        {
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            _orders = new Order
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = UserId,
                RestaurantId = resaurantId,
                TotalAmount = 4000
            };
        }

        public Order Build()
        {
            return _orders;
        }
    }
}
