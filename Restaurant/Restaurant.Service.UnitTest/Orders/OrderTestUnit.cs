using FluentAssertions;
using Moq;
using Restaurants.Contracts.Interface;
using restaurants.Services.Orders.Contracts.Dtos;
using restaurants.Services.Orders.Contracts;
using restaurants.Services.Orders;
using restaurants.Services.Resturants.Contracts.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurant.Entitis.Orders;
using restaurant.Entitis.Users;
using restaurants.Services.Orders.Contracts.Exeptions;
using Restaurant.Test.Tools.Instructure.DataBaseConfig;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Orders;
using Xunit.Sdk;
using restaurants.Services.Resturants.Contracts.Dtos;
using restaurants.Services.Users.Contracts.Dtos;
using restaurants.Services.Resturants.Contracts;
using restaurants.persistence.EF.Restaurants;
using restaurants.Services.Resturants;
using Microsoft.EntityFrameworkCore;
using Restaurant.Test.Tools.Orders;
using restaurant.Entitis.Restaurants;
using Restaurant.Test.Tools.Restaurants;
using Restaurant.Test.Tools.Users;

namespace Restaurant.Service.UnitTest.Orders
{
    public class OrderTestUnit
    {
        private readonly OrdersServices _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;

        public OrderTestUnit()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = OrdersServiceFactory.Create(_context);
        }

        [Fact]
        public async Task Add_New_orders_properly()
        {
            var restaurant = new RestaurantBuilder().Bulid();
            var user = new UserBuilder().Build();
            _context.Save(restaurant);
            _context.Save(user);
            var dto =AddOrdersDtoFactory.Create(user.Id,restaurant.Id);
          
            await _sut.Add(dto);
         
            var actual = _readContext.Orders.Single();
            actual.Notes.Should().Be(dto.Notes);
            actual.OrderDate.Should().Be(dto.OrderDate);
            actual.UserId.Should().Be(dto.UserId);
            actual.RestaurantId.Should().Be(dto.RestaurantId);
            actual.TotalAmount.Should().Be(dto.TotalAmount);
        }

        [Fact]
        public async Task Update_Existing_orders_Successfully()
        {
          
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var restaurant2 = new RestaurantBuilder().Bulid();
            var user2 = new UserBuilder().Build();
         _context.Save(restaurant2);
          _context.Save(user2);
          var order = new OrederBulider(restaurant2.Id,user2.Id).Build();
           
            _context.Save(order);
            var restaurant = new RestaurantBuilder().Bulid();
            var user = new UserBuilder().Build();
                _context.Save(restaurant);
                _context.Save(user);
                var dto = UpdateOrderDtoFactory.Create(user.Id, restaurant.Id);
            await _sut.Update(order.Id, dto);
            var actual = _readContext.Orders.First(_ => _.Id == order.Id);
            actual.Notes.Should().Be(dto.Notes);
            actual.OrderDate.Should().Be(dto.OrderDate);
            actual.UserId.Should().Be(dto.UserId);
            actual.RestaurantId.Should().Be(dto.RestaurantId);
            actual.TotalAmount.Should().Be(dto.TotalAmount);
        }

        [Fact]
        public async Task GEt_Get_order_Successfully()
        {
            
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var restaurant2 = new RestaurantBuilder().Bulid();
            var user2 = new UserBuilder().Build();
            _context.Save(restaurant2);
            _context.Save(user2);
            var restaurant = new RestaurantBuilder().Bulid();
            var user = new UserBuilder().Build();
            _context.Save(restaurant);
            _context.Save(user);
            var order1 = new OrederBulider(restaurant2.Id,user2.Id).Build();

            var order2 =new OrederBulider(restaurant.Id, user.Id).Build();
            _context.Save(order1);
            _context.Save(order2);
            var actual = await _sut.GetAll();
            actual.Count.Should().Be(2);
        }


        [Fact]
        public async Task remove_remove_orders_Successfully()
        {
            
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);

            var restaurant = new RestaurantBuilder().Bulid();
            var user = new UserBuilder().Build();
            _context.Save(restaurant);
            _context.Save(user);
            var order1 = new OrederBulider(restaurant.Id, user.Id).Build();
            _context.Save(order1);
            _sut.Delete(order1.Id);
            var actual = _readContext.Orders.Any();
            actual.Should().BeFalse();
        }

        [Fact]
        public async Task Update_throws_OrdersIsNotExistToUpdateException()
        {
            var Id = 8;
            
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);

            var restaurant = new RestaurantBuilder().Bulid();
            var user = new UserBuilder().Build();
            _context.Save(restaurant);
            _context.Save(user);
            var dto = UpdateOrderDtoFactory.Create(user.Id, restaurant.Id);
            var actual = () => _sut.Update(Id, dto);
            await actual.Should().ThrowExactlyAsync<OrderIsNotExistToUpadateException>();
        }

        
       
    }

}
