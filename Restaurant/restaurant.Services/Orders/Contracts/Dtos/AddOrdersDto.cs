using restaurant.Entitis.Restaurants;
using restaurant.Entitis.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.Services.Orders.Contracts.Dtos
{
    public class AddOrdersDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public string Notes { get; set; }
    }
}
