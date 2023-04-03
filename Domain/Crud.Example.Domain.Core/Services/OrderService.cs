using AutoMapper;
using Crud.Example.Domain.Dtos;
using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Domain.Services;

namespace Crud.Example.Domain.Core.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Corresponde al tipo de interfaz de tipo IDealerRepository.
        /// </summary>
        /// <param name="orderRepository"></param>
        public OrderService(IOrderRepository orderRepository,
                            IMapper mapper)
                            : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
         }

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            var ordersDto = _mapper.Map<OrderDto>(order);
            return ordersDto;
        }
    }
}