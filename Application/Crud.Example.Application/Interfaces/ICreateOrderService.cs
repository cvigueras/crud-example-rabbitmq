using Crud.Example.Domain.Dtos;
using Crud.Example.Domain.Entities;

namespace Crud.Example.Main.Interfaces
{
    public interface ICreateOrderService
    {
        void CreateOrder(Order order);
        OrderDto GetOrderById(int id);
    }
}
