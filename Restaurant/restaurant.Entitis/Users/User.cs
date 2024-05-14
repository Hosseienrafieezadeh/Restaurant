using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurant.Entitis.enums;
using restaurant.Entitis.Orders;
using restaurant.Entitis.Restaurants;

namespace restaurant.Entitis.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public UserType UserType { get; set; }
        public int? RestaurantId { get; set; } // Nullable int
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
