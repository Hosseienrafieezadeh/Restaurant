using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.Entitis.Restaurants;
using restaurants.Services.Resturants.Contracts;
using restaurants.Services.Resturants.Contracts.Dtos;

namespace restaurants.persistence.EF.Restaurants
{
    public class EFRestaurantRepozitory: RestaurantRepozitory
    {
        private readonly EFDataContext _context;

        public EFRestaurantRepozitory(EFDataContext context)
        {
            _context = context;
        } 
        public void Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
        }

        public void Delete(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
        }

        public void Update(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
        }

        public Restaurant? Find(int id)
        {
            return _context.Restaurants.FirstOrDefault(_ => _.Id == id);
        }

        public Restaurant? FindName(string name)
        {
            return _context.Restaurants.FirstOrDefault(_ => _.Name == name);
        }

        public async Task<List<GetRestaurantDto>> GetAll()
        {
            IQueryable<Restaurant> query = _context.Restaurants;
            List<GetRestaurantDto> restaurants = await query.Select(restaurant => new GetRestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                ContactEmail = restaurant.ContactEmail,
                ContactNumber = restaurant.ContactNumber
            }).ToListAsync();
            return restaurants;
        }

        //public bool IsExistUser(int userId)
        //{
        //    return _context.Users.Any(_ => _.Id==userId);
        //}

        //public bool IsExistOrder(int orderId)
        //{
        //    return _context.Orders.Any(_ => _.Id == orderId);
        //}
    }
}
