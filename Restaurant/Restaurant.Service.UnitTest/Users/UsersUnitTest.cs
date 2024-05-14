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
using restaurants.Services.Resturants.Contracts;
using Microsoft.EntityFrameworkCore;
using Restaurant.Test.Tools.Users;

namespace Restaurant.Service.UnitTest.Users
{
    
    public class UsersUnitTest
    {
        private readonly usersService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;

        public UsersUnitTest()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = UserServiceFactory.Create(_context);
        }

        [Fact]
        public async Task Add_New_User_properly()
        {
            var dto = AdduserDtoFactory.Create();
          
            await _sut.Add(dto);
            var actual = _readContext.Users.Single();
            actual.Password.Should().Be(dto.Password);
            actual.UserType.Should().Be(dto.UserType);
            actual.Username.Should().Be(dto.Username);
            actual.RestaurantId.Should().Be(dto.RestaurantId);
        }

        [Fact]
        public async Task Update_Existing_user_Successfully()
        {
            var user =new UserBuilder().Build();

            _context.Save(user);
            var updateDto = UserUpdateDtoFactory.Create();
            await _sut.Update(user.Id, updateDto);
            var actual = _readContext.Users.First(_ => _.Id == user.Id);
            actual.Password.Should().Be(updateDto.Password);
            actual.UserType.Should().Be(updateDto.UserType);
            actual.Username.Should().Be(updateDto.Username);
            actual.RestaurantId.Should().Be(updateDto.RestaurantId);
        }

        [Fact]
        public async Task GEt_Get_users_Successfully()
        {
           
            var user1 = new UserBuilder().Build();
            var user2 = new UserBuilder().Build();
            _context.Save(user1);
            _context.Save(user2);
          
            var actual = await _sut.GetAll();
            actual.Count.Should().Be(2);
        }

        [Fact]
        public async Task remove_remove_user_Successfully()
        {
           
            var user = new UserBuilder().Build();
            _context.Save(user);
            _sut.Delete(user.Id);
            var actual = _readContext.Users.Any();
            actual.Should().BeFalse();
        }

        [Fact]
        public async Task Update_throsws_UserIsNotExistToUpdateException()
        {
            var Id = 8;
        
            var update = UserUpdateDtoFactory.Create();

            var actual = () => _sut.Update(Id, update);
            await actual.Should().ThrowExactlyAsync<UsersIsNotExistToUpdateException>();
        }
        [Fact]
        public async Task Add_throsws_UserIsNotExistException()
        {
            var dto1 = AdduserDtoFactory.Create();
         
            await _sut.Add(dto1);

            var dto2 = AdduserDtoFactory.Create();
            var actual = async () => await _sut.Add(dto2);
            await actual.Should().ThrowAsync<UserIsNotExistException>();
        }
    }
}
