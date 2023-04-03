using System.Text.Json;
using Crud.Example.Domain.Events;
using Crud.Example.Infrastructure.Messaging.Services;

namespace Crud.Example.Domain.Handler
{
    public class ShopExpiredHandler : IDomainHandler<ShopExpiredEvent>
    {
        private readonly AmpqService _aMQPService;
        private const string _exchangeName = "expiredShopsExchange";
        private const string _queueName = "expiredShopsQueue";
        
        public ShopExpiredHandler(AmpqService aMQPService)
        {
            _aMQPService = aMQPService;
        }
        public void Handle(ShopExpiredEvent @event)
        {
            if (@event != null)
            {
                var jsonPayload = JsonSerializer.Serialize(@event.Shop);
                _aMQPService.PublishMessage(jsonPayload, _exchangeName,_queueName);
            }
        }
    }
}
