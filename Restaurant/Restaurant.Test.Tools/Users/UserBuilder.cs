using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurant.Entitis.Users;
using restaurants.Services.Users.Contracts.Dtos;

namespace Restaurant.Test.Tools.Users
{
    public class UserBuilder
    {
        private readonly User _user;
        public UserBuilder(string? username = null)
        {
            _user= new User()
            {
                Username = username ?? "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
        }

        public User Build()
        {
            return _user;
        }
    }
}
