using Crud.Example.Domain.Dtos;

namespace Crud.Example.Main.Interfaces
{
    public interface IGetOrderService
    {
        OrderDto GetOrderById(int id);
    }
}