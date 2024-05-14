using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurants.Services.Resturants.Contracts.Dtos;
using restaurants.Services.Users;
using restaurants.Services.Users.Contracts;
using restaurants.Services.Users.Contracts.Dtos;

namespace restaurants.RestApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly usersService _service;


        public UserController( usersService service)
        {
            _service = service;
           
        }
        [HttpPost]
        public async Task Add([FromBody] AddUserDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateUsersDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetAllUserDto>> GetAll()
        {
            return await _service.GetAll();
        }
    }
}
