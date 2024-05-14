using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurants.Services.Resturants.Contracts;
using restaurants.Services.Resturants.Contracts.Dtos;

namespace restaurants.RestApi.Controllers.Restaurant
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _service;

        public RestaurantController(RestaurantService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddRestaurantDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetRestaurantDto>> GetAll()
        {
            return await _service.GetAll();
        }
    }
}
