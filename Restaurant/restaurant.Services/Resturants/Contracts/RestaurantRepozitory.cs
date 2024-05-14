using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurant.Entitis.Restaurants;
using restaurants.Services.Resturants.Contracts.Dtos;

namespace restaurants.Services.Resturants.Contracts
{
    public interface RestaurantRepozitory
    {
        void Add(Restaurant restaurant);
        void Delete(Restaurant restaurant);
        void Update(Restaurant restaurant);
        Restaurant? Find(int id);
        Restaurant? FindName(string name);
        Task<List<GetRestaurantDto>> GetAll();
        //bool IsExistUser(int userId);
        //bool IsExistOrder(int orderId);

    }
}
