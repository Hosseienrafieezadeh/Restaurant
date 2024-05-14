using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurant.Entitis.Orders;
using restaurants.Services.Orders.Contracts.Dtos;

namespace restaurants.Services.Orders.Contracts
{
    public interface OrdersRepozitory
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        Order? Find(int  id);
        Task<List<GetOrdersDto>> GetAll();
        bool IsExistUser(int userId);
        bool IsExistRestaurant(int restaurantId);

    }
}
