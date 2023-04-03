using Crud.Example.Domain.Dtos;
using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IOrderService _orderService;

        public CreateOrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void CreateOrder(Order order)
        {
            _orderService.CreateOrder(order);
        }

        public OrderDto GetOrderById(int id)
        {
            return _orderService.GetOrderById(id);
        }
    }
}
