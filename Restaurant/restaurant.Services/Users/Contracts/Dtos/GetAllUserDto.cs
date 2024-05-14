using restaurant.Entitis.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.Services.Users.Contracts.Dtos
{
    public class GetAllUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public UserType UserType { get; set; }
        public int? RestaurantId { get; set; }
    }
}
