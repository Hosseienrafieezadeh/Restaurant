using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Contracts.Interface;
using restaurant.Entitis.Orders;
using restaurants.Services.Orders.Contracts;
using restaurants.Services.Orders.Contracts.Dtos;
using restaurants.Services.Orders.Contracts.Exeptions;
using restaurants.Services.Resturants.Contracts.Exeptions;
using restaurants.Services.Users.Contracts;
using restaurants.Services.Users.Contracts.Excptions;

namespace restaurants.Services.Orders
{
    public class OrdersAppServices:OrdersServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly OrdersRepozitory _repozitory;

        public OrdersAppServices(OrdersRepozitory repozitory, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repozitory = repozitory;
        }
        public async Task Add(AddOrdersDto dto)
        {
            if (!_repozitory.IsExistRestaurant(dto.RestaurantId))
            {
                throw new RestaurantsIsNotExistException();
            }

            if (!_repozitory.IsExistUser(dto.UserId))
            {
                throw new UserIsNotExistException();
            }
            var order = new Order
            {
                Notes = dto.Notes,
                OrderDate = dto.OrderDate,
                UserId = dto.UserId,
                RestaurantId = dto.RestaurantId,
                TotalAmount = dto.TotalAmount

            };
            _repozitory.Add(order);
            await _unitOfWork.Complete();
        }

        public async Task Update(int id, UpdateOrdersDto dto)
        {
            var order = _repozitory.Find(id);
            if (order==null)
            {
                throw new OrderIsNotExistToUpadateException();
            }
            order.Notes=dto.Notes;
            order.OrderDate=dto.OrderDate;
            order.UserId=dto.UserId;
            order.RestaurantId=dto.RestaurantId;
            order.TotalAmount=dto.TotalAmount;
            _repozitory.Update(order);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var order=_repozitory.Find(id);
            if (order==null)
            {
                throw new OrderIsNotExistException();
            }
            _repozitory.Delete(order);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetOrdersDto>> GetAll()
        {
            return await _repozitory.GetAll();
        }
    }
}
