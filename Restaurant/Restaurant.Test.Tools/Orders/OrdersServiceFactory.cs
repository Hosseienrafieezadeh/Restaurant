using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Orders;
using restaurants.Services.Orders;
using restaurants.Services.Orders.Contracts;

namespace Restaurant.Test.Tools.Orders
{
    public static class OrdersServiceFactory
    {
        public static OrdersServices Create(EFDataContext context)
        {
            return new OrdersAppServices(new EFOrdersRepozitory(context), new EFUnitOfWork(context));
        }
    }
}
