using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Orders.Contracts.Dtos;

namespace restaurants.Services.Orders.Contracts
{
    public interface OrdersServices
    {
        Task Add(AddOrdersDto dto);
        Task Update(int id,UpdateOrdersDto dto);
        Task Delete(int id);
        Task<List<GetOrdersDto>> GetAll();
    }
}
