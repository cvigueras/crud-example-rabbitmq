using Crud.Example.Api.Filters;
using Crud.Example.Domain.Entities;
using Crud.Example.Main.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Example.Api.Controllers
{
    [ServiceFilter(typeof(BearerAuthenticationFilterAttribute))]
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGetOrderService _getOrderService;
        private readonly ICreateOrderService _createOrderService;

        public OrderController(IGetOrderService getOrderService,
                                ICreateOrderService createOrderService)
        {
            _getOrderService = getOrderService;
            _createOrderService = createOrderService;
        }

        [HttpPost]
        [Route("getorders")]
        public IActionResult GetorderById(int id)
        {
            var orders = _getOrderService.GetOrderById(id);
            return Ok(orders);
        }

        [HttpPost]
        [Route("createorder")]
        public IActionResult CreateOrder(Order order)
        {
            _createOrderService.CreateOrder(order);
            return Ok();
        }
    }
}
