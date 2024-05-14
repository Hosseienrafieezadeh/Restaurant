using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restaurants.Services.Users.Contracts.Dtos;

namespace restaurants.Services.Users.Contracts
{
    public interface usersService
    {
        Task Add(AddUserDto dto);
        Task Delete(int id);
        Task Update(int id, UpdateUsersDto dto);
        Task<List<GetAllUserDto>> GetAll();

    }
}
