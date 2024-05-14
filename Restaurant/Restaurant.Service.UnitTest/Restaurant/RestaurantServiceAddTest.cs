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

namespace Restaurant.Service.UnitTest.Restaurant
{
    public class RestaurantServiceAddTest
    {
        [Fact]
        public async Task Add_New_Restaurants_properly()
        {
            // Arrange
            var dto = new AddRestaurantDto
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new RestaurantAppService(new EFRestaurantRepozitory(context), new EFUnitOfWork(context));

            // Act
            await sut.Add(dto);

            // Assert
            var actual = readContext.Restaurants.Single();
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
           
            var restaurant = new restaurant.Entitis.Restaurants.Restaurant()
            {
                
                Name = "updated_name",
                Category = "updated_category",
                ContactEmail = "updated_email@example.com",
                ContactNumber = "1234567890",
                Description = "updated_description",
                HasDelivery = false,
            };

            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new RestaurantAppService(new EFRestaurantRepozitory(context), new EFUnitOfWork(context));

            // Act
            context.Save(restaurant);
            var updateDto = new UpdateRestaurantDto()
            {
                Name = "updated_namee",
                Category = "updated_categoreyee",
                ContactEmail = "updated_email@exampleeee.com",
                ContactNumber = "123456789000",
                Description = "updated_description",
                HasDelivery = true,
            };
            await sut.Update(restaurant.Id, updateDto); 
            // Assert
            var actual = readContext.Restaurants.First(_ => _.Id == restaurant.Id); 
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
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var restaurant1 = new restaurant.Entitis.Restaurants.Restaurant()
            {

                Name = "updated_name",
                Category = "updated_category",
                ContactEmail = "updated_email@example.com",
                ContactNumber = "1234567890",
                Description = "updated_description",
                HasDelivery = false,
            }; 
            var restaurant2 = new restaurant.Entitis.Restaurants.Restaurant()
            {

                Name = "updated_name",
                Category = "updated_category",
                ContactEmail = "updated_email@example.com",
                ContactNumber = "1234567890",
                Description = "updated_description",
                HasDelivery = false,
            };
            context.Save(restaurant1);
            context.Save(restaurant2);
            var sut = new RestaurantAppService(new EFRestaurantRepozitory(context), new EFUnitOfWork(context));
            var acutal =await sut.GetAll();
            acutal.Count().Should().Be(2);
        }
        [Fact]
        public async Task remove_remove_Restaurant_Successfully()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var restaurant1 = new restaurant.Entitis.Restaurants.Restaurant()
            {

                Name = "updated_name",
                Category = "updated_category",
                ContactEmail = "updated_email@example.com",
                ContactNumber = "1234567890",
                Description = "updated_description",
                HasDelivery = false,
            };
            var sut = new RestaurantAppService(new EFRestaurantRepozitory(context), new EFUnitOfWork(context));
            context.Save(restaurant1);
           
            sut.Delete(restaurant1.Id);
            var actual = readContext.Restaurants.Any();
            actual.Should().BeFalse();
        }
        [Fact]
        public async Task Update_throsws_RestaurantsIsNotExistToUpdateException()
        {
            var Id = 8;
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var update = new UpdateRestaurantDto
            {

                Name = "updated_name",
                Category = "updated_category",
                ContactEmail = "updated_email@example.com",
                ContactNumber = "1234567890",
                Description = "updated_description",
                HasDelivery = false,
            };
            var sut = new RestaurantAppService(new EFRestaurantRepozitory(context), new EFUnitOfWork(context));
            var actual = () => sut.Update(Id, update);
            await actual.Should().ThrowExactlyAsync<RestaurantsIsNotExistToUpdateException>();
        }
        
        [Fact]
        public async Task Add_throws_RestaurantsIsNotExistException()
        {
            var dto1 = new AddRestaurantDto
            {
                Name = "ali",
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var sut = new RestaurantAppService(new EFRestaurantRepozitory(context), new EFUnitOfWork(context));

            // Act
            await sut.Add(dto1);

            var dto2 = new AddRestaurantDto
            {
                Name = "ali", // Same name as dto1
                Category = "fastFood",
                ContactEmail = "Hossein.rf27@gmailcom",
                ContactNumber = "09174554121",
                Description = "noting",
                HasDelivery = true,
            };

            // Assert
            var actual = async () => await sut.Add(dto2);
            await actual.Should().ThrowAsync<RestaurantsIsNotExistException>();
        }
    }
}
