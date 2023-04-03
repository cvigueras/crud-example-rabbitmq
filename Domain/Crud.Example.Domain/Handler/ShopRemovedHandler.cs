using System.Text.Json;
using Crud.Example.Domain.Events;
using Crud.Example.Infrastructure.Messaging.Services;

namespace Crud.Example.Domain.Handler
{
    public class ShopRemovedHandler : IDomainHandler<ShopRemovedEvent>
    {
        private readonly AmpqService _aMQPService;
        private const string _exchangeName = "removedShopsExchange";
        private const string _queueName = "removedShopsQueue";
        public ShopRemovedHandler(AmpqService aMQPService)
        {
            _aMQPService = aMQPService;
        }
        public void Handle(ShopRemovedEvent @event)
        {
            var jsonPayload = JsonSerializer.Serialize(@event.Shop);
            _aMQPService.PublishMessage(jsonPayload, _exchangeName, _queueName);
        }
    }
}