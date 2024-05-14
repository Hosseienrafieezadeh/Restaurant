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

namespace Restaurant.Service.UnitTest.Orders
{
    public class OrderTestUnit
    {
        [Fact]
        public async Task Add_New_orders_properly()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new OrdersAppServices(new EFOrdersRepozitory(context), new EFUnitOfWork(context));
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var restaurant = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(restaurant);
            context.Save(user);
            var dto = new AddOrdersDto
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user.Id,
                RestaurantId =restaurant.Id,
                TotalAmount = 4000
            };
          
            await sut.Add(dto);
         
            var actual = readContext.Orders.Single();
            actual.Notes.Should().Be(dto.Notes);
            actual.OrderDate.Should().Be(dto.OrderDate);
            actual.UserId.Should().Be(dto.UserId);
            actual.RestaurantId.Should().Be(dto.RestaurantId);
            actual.TotalAmount.Should().Be(dto.TotalAmount);
        }

        [Fact]
        public async Task Update_Existing_orders_Successfully()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new OrdersAppServices(new EFOrdersRepozitory(context), new EFUnitOfWork(context));
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var restaurant2 = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user2 = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
          context.Save(restaurant2);
          context.Save(user2);
            var order = new Order
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user2.Id,
                RestaurantId = restaurant2.Id ,
                TotalAmount = 4000
            };
           
            context.Save(order);
            var restaurant = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(restaurant);
            context.Save(user);
            var dto = new UpdateOrdersDto
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user.Id,
                RestaurantId = restaurant.Id,
                TotalAmount = 4000
            };
            await sut.Update(order.Id, dto);
            var actual = readContext.Orders.First(_ => _.Id == order.Id);
            actual.Notes.Should().Be(dto.Notes);
            actual.OrderDate.Should().Be(dto.OrderDate);
            actual.UserId.Should().Be(dto.UserId);
            actual.RestaurantId.Should().Be(dto.RestaurantId);
            actual.TotalAmount.Should().Be(dto.TotalAmount);
        }

        [Fact]
        public async Task GEt_Get_order_Successfully()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new OrdersAppServices(new EFOrdersRepozitory(context), new EFUnitOfWork(context));
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var restaurant2 = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user2 = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(restaurant2);
            context.Save(user2);
            var restaurant = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(restaurant);
            context.Save(user);
            var order1 = new Order
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user2.Id,
                RestaurantId = restaurant2.Id,
                TotalAmount = 4000
            };

            var order2 = new Order
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user.Id,
                RestaurantId = restaurant.Id,
                TotalAmount = 4000
            };
            context.Save(order1);
            context.Save(order2);
            var actual = await sut.GetAll();
            actual.Count.Should().Be(2);
        }


        [Fact]
        public async Task remove_remove_orders_Successfully()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var sut = new OrdersAppServices(new EFOrdersRepozitory(context), new EFUnitOfWork(context));
            var restaurant = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(restaurant);
            context.Save(user);
            var order1 = new Order
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user.Id,
                RestaurantId = restaurant.Id,
                TotalAmount = 4000
            };
            context.Save(order1);
            sut.Delete(order1.Id);
            var actual = readContext.Orders.Any();
            actual.Should().BeFalse();
        }

        [Fact]
        public async Task Update_throws_OrdersIsNotExistToUpdateException()
        {
            var Id = 8;
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var orderDate = new DateTime(2015, 3, 10, 2, 15, 10);
            var sut = new OrdersAppServices(new EFOrdersRepozitory(context), new EFUnitOfWork(context));
            var restaurant = new restaurant.Entitis.Restaurants.Restaurant
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(restaurant);
            context.Save(user);
            var dto = new UpdateOrdersDto
            {
                Notes = "string",
                OrderDate = orderDate,
                UserId = user.Id,
                RestaurantId = restaurant.Id,
                TotalAmount = 4000
            };
            var actual = () => sut.Update(Id, dto);
            await actual.Should().ThrowExactlyAsync<OrderIsNotExistToUpadateException>();
        }

        
       
    }

}
