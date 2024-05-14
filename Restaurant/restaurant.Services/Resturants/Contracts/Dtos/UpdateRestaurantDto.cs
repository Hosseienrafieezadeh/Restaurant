using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.Services.Resturants.Contracts.Dtos
{
   public class UpdateRestaurantDto
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
        [Required]
        public string Category { get; set; } = default!;
        [Required]
        public bool HasDelivery { get; set; }
        [Required]
        public string? ContactEmail { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
    }
}
