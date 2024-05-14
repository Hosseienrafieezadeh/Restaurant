using restaurants.Services.Resturants.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurants.Services.Resturants.Contracts
{
    public interface RestaurantService
    {
        Task Add(AddRestaurantDto dto);
        Task Delete(int id);
        Task<List<GetRestaurantDto>> GetAll();
        Task Update(int id, UpdateRestaurantDto updateDto);
    }
}
