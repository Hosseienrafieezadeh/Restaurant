using restaurant.Entitis.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.Services.Users.Contracts.Dtos
{
    public class AddUserDto
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public int? RestaurantId { get; set; }
    }
}
