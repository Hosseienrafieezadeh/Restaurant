using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.Entitis.Users;
using restaurants.Services.Users.Contracts;
using restaurants.Services.Users.Contracts.Dtos;

namespace restaurants.persistence.EF.Users
{
    public class EFUsersRepozitory:UsersRepozitory
    {
        private readonly EFDataContext _context;

        public EFUsersRepozitory(EFDataContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        
        }

        public User? Find(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? FindName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Username == name);
        }

        public bool IsExistRestaurant(int restaurantId)
        {
            return _context.Restaurants.Any(_ => _.Id == restaurantId);
        }

        public async Task<List<GetAllUserDto>> GetAll()
        {
            IQueryable<User>query=_context.Users;
            List<GetAllUserDto> users = await query.Select(user => new GetAllUserDto
            {
                Id = user.Id,
                Password = user.Password,
                RestaurantId = user.RestaurantId,
                Username = user.Username,
                UserType = user.UserType
            }).ToListAsync();
            return users;
        }
    }
}
