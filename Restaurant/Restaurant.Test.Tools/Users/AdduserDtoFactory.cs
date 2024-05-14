using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Users.Contracts.Dtos;

namespace Restaurant.Test.Tools.Users
{
    public static class AdduserDtoFactory
    {
        public static AddUserDto Create(string?username=null)
        {
            return new AddUserDto()
            {
                Username =username ??"ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
        }
    }
}
