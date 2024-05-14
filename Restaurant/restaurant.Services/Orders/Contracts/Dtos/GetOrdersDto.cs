﻿using restaurant.Entitis.Restaurants;
using restaurant.Entitis.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.Services.Orders.Contracts.Dtos
{
    public class GetOrdersDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
    }
}
