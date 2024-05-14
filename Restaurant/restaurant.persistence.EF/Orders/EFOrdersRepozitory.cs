using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.Entitis.Orders;
using restaurant.Entitis.Users;
using restaurants.Services.Orders.Contracts;
using restaurants.Services.Orders.Contracts.Dtos;

namespace restaurants.persistence.EF.Orders
{
    public class EFOrdersRepozitory:OrdersRepozitory
    {
        private readonly EFDataContext _context;

        public EFOrdersRepozitory(EFDataContext context)
        {
            _context = context;
        }
        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }


        public Order? Find(int id)
        {
            return _context.Orders.FirstOrDefault(_ => _.Id == id);
        }

        public async Task<List<GetOrdersDto>> GetAll()
        {
            IQueryable<Order> query = _context.Orders;
            List<GetOrdersDto> orders = await query.Select(order => new GetOrdersDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                Notes = order.Notes,
                OrderDate = order.OrderDate,
                RestaurantId = order.RestaurantId,
                UserId = order.UserId

            }).ToListAsync();
            return orders;
        }

        public bool IsExistUser(int userId)
        {
            return _context.Users.Any(_ => _.Id == userId);
        }

        public bool IsExistRestaurant(int restaurantId)
        {
            return _context.Restaurants.Any(_ => _.Id == restaurantId);
        }
    }
}
