using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurants.Services.Orders.Contracts;
using restaurants.Services.Orders.Contracts.Dtos;
using restaurants.Services.Resturants.Contracts.Dtos;

namespace restaurants.RestApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersServices _service;

        public OrdersController(OrdersServices services)
        {
            _service = services;
        }
        [HttpPost]
        public async Task Add([FromBody] AddOrdersDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateOrdersDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetOrdersDto>> GetAll()
        {
            return await _service.GetAll();
        }
    }
}
