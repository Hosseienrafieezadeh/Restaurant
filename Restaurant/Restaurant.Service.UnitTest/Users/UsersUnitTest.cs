using FluentAssertions;
using Moq;
using restaurant.Entitis.enums;
using restaurant.Entitis.Users;
using Restaurants.Contracts.Interface;
using restaurants.Services.Resturants.Contracts.Exeptions;
using restaurants.Services.Users.Contracts.Dtos;
using restaurants.Services.Users.Contracts;
using restaurants.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Resturants.Contracts.Dtos;
using restaurants.Services.Users.Contracts.Excptions;
using Restaurant.Test.Tools.Instructure.DataBaseConfig;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Users;
using restaurants.persistence.EF.Restaurants;
using restaurants.Services.Resturants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.Service.UnitTest.Users
{
    public class UsersUnitTest
    {
        [Fact]
        public async Task Add_New_User_properly()
        {
            var dto = new AddUserDto()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
            await sut.Add(dto);
            var actual = readContext.Users.Single();
            actual.Password.Should().Be(dto.Password);
            actual.UserType.Should().Be(dto.UserType);
            actual.Username.Should().Be(dto.Username);
            actual.RestaurantId.Should().Be(dto.RestaurantId);
        }

        [Fact]
        public async Task Update_Existing_user_Successfully()
        {
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
            context.Save(user);
            var updateDto = new UpdateUsersDto()
            {
                Username = "ali2",
                Password = "123456789",
                UserType = 0,
                RestaurantId = 3,
            };
            await sut.Update(user.Id, updateDto);
            var actual = readContext.Users.First(_ => _.Id == user.Id);
            actual.Password.Should().Be(updateDto.Password);
            actual.UserType.Should().Be(updateDto.UserType);
            actual.Username.Should().Be(updateDto.Username);
            actual.RestaurantId.Should().Be(updateDto.RestaurantId);
        }

        [Fact]
        public async Task GEt_Get_users_Successfully()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var user1 = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            var user2 = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            context.Save(user1);
            context.Save(user2);
            var sut = new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
            var actual = await sut.GetAll();
            actual.Count.Should().Be(2);
        }

        [Fact]
        public async Task remove_remove_user_Successfully()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var user = new User()
            {
                Username = "ali",
                Password = "12345678",
                UserType = 0,
                RestaurantId = 4,
            };
            var sut = new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
            context.Save(user);
            sut.Delete(user.Id);
            var actual = readContext.Users.Any();
            actual.Should().BeFalse();
        }

        [Fact]
        public async Task Update_throsws_UserIsNotExistToUpdateException()
        {
            var Id = 8;
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var update = new UpdateUsersDto()
            {
                Username = "ali",
                Password = "123456789",
                UserType = 0,
                RestaurantId = 3,
            };
            var sut = new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
            var actual = () => sut.Update(Id, update);
            await actual.Should().ThrowExactlyAsync<UsersIsNotExistToUpdateException>();
        }
        [Fact]
        public async Task Add_throsws_UserIsNotExistException()
        {
            var dto1 = new AddUserDto
            {
                Username = "ali2",
                Password = "123456789",
                UserType = 0,
                RestaurantId = 3,
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var sut = new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
            await sut.Add(dto1);

            var dto2 = new AddUserDto
            {
                Username = "ali2",
                Password = "123456789",
                UserType = 0,
                RestaurantId = 3,
            };
            var actual = async () => await sut.Add(dto2);
            await actual.Should().ThrowAsync<UserIsNotExistException>();
        }
    }
}
