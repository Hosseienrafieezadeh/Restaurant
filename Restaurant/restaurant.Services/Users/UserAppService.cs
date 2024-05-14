using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Contracts.Interface;
using restaurants.Services.Users.Contracts;
using restaurants.Services.Users.Contracts.Dtos;
using restaurant.Entitis.Users;
using restaurants.Services.Resturants.Contracts.Exeptions;
using restaurants.Services.Users.Contracts.Excptions;

namespace restaurants.Services.Users
{
    public class UserAppService:usersService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UsersRepozitory _repozitory;

        public UserAppService(UsersRepozitory repozitory, UnitOfWork uitOfWork)
        {
            _unitOfWork=uitOfWork;
            _repozitory = repozitory;
        }

        public async Task Add(AddUserDto dto)
        {
            if (_repozitory.FindName(dto.Username) != null)
            {
                throw new UserIsNotExistException();
            }
            //if (_repozitory.IsExistRestaurant(dto.RestaurantId))
            //{
            //    throw new RestaurantsIsNotExistException();
            //}
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                UserType = dto.UserType,
                RestaurantId = dto.RestaurantId, // Assuming you have a RestaurantId in your DTO,

            };

            _repozitory.Add(user);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var user = _repozitory.Find(id);
            if (user == null)
            {
                throw new UserIsNotExistException();
            }

            _repozitory.Delete(user);
            await _unitOfWork.Complete();
        }

        public async Task Update(int id, UpdateUsersDto dto)
        {
            var user = _repozitory.Find(id);
            if (user == null)
            {
                throw new UsersIsNotExistToUpdateException();
            }

            // Update user properties
            user.Username = dto.Username;
            user.Password = dto.Password;
            user.UserType = dto.UserType;
            user.RestaurantId = dto.RestaurantId;

            _repozitory.Update(user);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetAllUserDto>> GetAll()
        {
            return await _repozitory.GetAll();
        }
    }
}
