using Crud.Example.Domain.Dtos;
using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Services
{
    public interface IOrderService : IBaseService<Order>
    {
        void CreateOrder(Order order);
        OrderDto GetOrderById(int id);
    }
}