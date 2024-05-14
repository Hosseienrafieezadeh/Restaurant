using restaurant.Entitis.Restaurants;
using restaurants.Services.Resturants.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Users.Contracts.Dtos;
using restaurant.Entitis.Users;

namespace restaurants.Services.Users.Contracts
{
    public interface UsersRepozitory
    {
        void Add(User user);
        void Delete(User user);
        void Update(User user);
        User? Find(int id);
        User? FindName(string name);
        bool IsExistRestaurant(int restaurantId);
        Task<List<GetAllUserDto>> GetAll();
    }
}
