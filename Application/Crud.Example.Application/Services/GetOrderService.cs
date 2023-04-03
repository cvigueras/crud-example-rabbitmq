using Crud.Example.Domain.Dtos;
using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderService _orderService;

        public GetOrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public OrderDto GetOrderById(int id)
        {
            return _orderService.GetOrderById(id);
        }
    }
}
