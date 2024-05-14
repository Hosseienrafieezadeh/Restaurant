using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Restaurant.Test.Tools.Instructure.DataBaseConfig;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Restaurants;
using restaurants.Services.Resturants;
using restaurants.Services.Resturants.Contracts.Dtos;
using Xunit;
using FluentAssertions;
using Moq;
using restaurants.Services.Resturants.Contracts.Exeptions;
using restaurants.Services.Resturants.Contracts;
using Microsoft.EntityFrameworkCore;
using Restaurant.Test.Tools.Restaurants;

namespace Restaurant.Service.UnitTest.Restaurant
{
    public class RestaurantServiceAddTest
    {
        private readonly RestaurantService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _reaContext;

        public RestaurantServiceAddTest()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _reaContext=db.CreateDataContext<EFDataContext>();
            _sut = RestaurantServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Add_New_Restaurants_properly()
        {
            // Arrange
            var dto = AddresturantDtoFactory.Create();
            // Act
            await _sut.Add(dto);

            // Assert
            var actual = _reaContext.Restaurants.Single();
            actual.Name.Should().Be(dto.Name);
            actual.Category.Should().Be(dto.Category);
            actual.ContactEmail.Should().Be(dto.ContactEmail);
            actual.ContactNumber.Should().Be(dto.ContactNumber);
            actual.Description.Should().Be(dto.Description);
            actual.HasDelivery.Should().Be(dto.HasDelivery);
        }
        [Fact]
        public async Task Update_Existing_Restaurant_Successfully()
        {
            // Arrange

            var restaurant = new RestaurantBuilder().Bulid();

            // Act
            _context.Save(restaurant);
            var updateDto = UpdateRestaurantDtoFactory.Create();
            await _sut.Update(restaurant.Id, updateDto); 
            // Assert
            var actual =_reaContext.Restaurants.First(_ => _.Id == restaurant.Id); 
            actual.Name.Should().Be(updateDto.Name);
            actual.Category.Should().Be(updateDto.Category);
            actual.ContactEmail.Should().Be(updateDto.ContactEmail);
            actual.ContactNumber.Should().Be(updateDto.ContactNumber);
            actual.Description.Should().Be(updateDto.Description);
            actual.HasDelivery.Should().Be(updateDto.HasDelivery);
        }

        [Fact]
        public async Task GEt_Get_Restaurant_Successfully()
        {
           
            var restaurant1 = new RestaurantBuilder().Bulid();
            var restaurant2 = new RestaurantBuilder().Bulid();
            _context.Save(restaurant1);
            _context.Save(restaurant2);
            
            var acutal =await _sut.GetAll();
            acutal.Count().Should().Be(2);
        }
        [Fact]
        public async Task remove_remove_Restaurant_Successfully()
        {
           
            var restaurant1 = new RestaurantBuilder().Bulid();

            _context.Save(restaurant1);
            _sut.Delete(restaurant1.Id);
            var actual = _reaContext.Restaurants.Any();
            actual.Should().BeFalse();
        }
        [Fact]
        public async Task Update_throsws_RestaurantsIsNotExistToUpdateException()
        {
            var Id = 8;

            var update = UpdateRestaurantDtoFactory.Create();
           
            var actual = () => _sut.Update(Id, update);
            await actual.Should().ThrowExactlyAsync<RestaurantsIsNotExistToUpdateException>();
        }
        
        [Fact]
        public async Task Add_throws_RestaurantsIsNotExistException()
        {
            var dto1 = AddresturantDtoFactory.Create();
       

            // Act
            await _sut.Add(dto1);

            var dto2 = AddresturantDtoFactory.Create();

            // Assert
            var actual = async () => await _sut.Add(dto2);
            await actual.Should().ThrowAsync<RestaurantsIsNotExistException>();
        }
    }
}
