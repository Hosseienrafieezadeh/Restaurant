using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurants.persistence.EF;
using restaurants.persistence.EF.Users;
using restaurants.Services.Users;
using restaurants.Services.Users.Contracts;

namespace Restaurant.Test.Tools.Users
{
    public static class UserServiceFactory
    {
        public static usersService Create(EFDataContext context)
        {
            return new UserAppService(new EFUsersRepozitory(context), new EFUnitOfWork(context));
        }
    }
}
