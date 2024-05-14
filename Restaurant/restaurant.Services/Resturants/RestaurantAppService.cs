using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Contracts.Interface;
using restaurant.Entitis.Restaurants;
using restaurants.Services.Resturants.Contracts;
using restaurants.Services.Resturants.Contracts.Dtos;
using restaurants.Services.Resturants.Contracts.Exeptions;

namespace restaurants.Services.Resturants
{
    public class RestaurantAppService : RestaurantService
    {
        private readonly RestaurantRepozitory _repozitory;
        private readonly UnitOfWork _unitOfWork;

        public RestaurantAppService(RestaurantRepozitory repozitory, UnitOfWork unitOfWork)
        {
            _repozitory = repozitory;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddRestaurantDto dto)
        {
            if (_repozitory.FindName(dto.Name) != null)
            {
                throw new RestaurantsIsNotExistException();
            }

            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Category = dto.Category,
                ContactEmail = dto.ContactEmail,
                ContactNumber = dto.ContactNumber,
                Description = dto.Description,
                HasDelivery = dto.HasDelivery,
            };
            _repozitory.Add(restaurant);
            await _unitOfWork.Complete();

        }

        public async Task Delete(int id)
        {
            var restaurant = _repozitory.Find(id);
            if (restaurant == null)
            {
                throw new RestaurantsIsNotExistException();
            }

            _repozitory.Delete(restaurant);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetRestaurantDto>> GetAll()
        {
            return await _repozitory.GetAll();
        }

        public async Task Update(int id, UpdateRestaurantDto updateDto)
        {
            var restaurant = _repozitory.Find(id);
            if (restaurant is null)
            {
                throw new RestaurantsIsNotExistToUpdateException();
            }

            restaurant.Name = updateDto.Name;
            restaurant.Category = updateDto.Category;
            restaurant.ContactEmail = updateDto.ContactEmail;
            restaurant.ContactNumber = updateDto.ContactNumber;
            restaurant.HasDelivery = updateDto.HasDelivery;
            restaurant.Description = updateDto.Description;
            _repozitory.Update(restaurant);
            await _unitOfWork.Complete();
        }
        
    }
}
