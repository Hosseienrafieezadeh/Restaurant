using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Restaurants;
using restaurants.Services.Resturants;
using restaurants.Services.Resturants.Contracts;

namespace Restaurant.Test.Tools.Restaurants
{
    public static class RestaurantServiceFactory
    {
        public static RestaurantService Create(EFDataContext Context)
        {
            return new RestaurantAppService(new EFRestaurantRepozitory(Context), new EFUnitOfWork(Context));
        }
    }
}
