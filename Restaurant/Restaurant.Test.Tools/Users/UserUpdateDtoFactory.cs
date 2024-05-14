using restaurants.Services.Users.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Test.Tools.Users
{
    public static class UserUpdateDtoFactory
    {
        public static UpdateUsersDto Create(string? username = null)
        {
            return new UpdateUsersDto()
            {
                Username = username ?? "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
        }
    }
}
