using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Crud.Example.Infrastructure.Messaging.Services
{
    public class CustomEventingBasicConsumer : AsyncEventingBasicConsumer
    {
        private readonly IModel _model;
        public CustomEventingBasicConsumer(IModel model) : base(model)
        {
            _model = model;
        }

        public override Task HandleBasicCancelOk(string consumerTag)
        {
            return base.HandleBasicCancelOk(consumerTag);
        }

        public override Task HandleBasicCancel(string consumerTag)
        {
            return base.HandleBasicCancel(consumerTag);
        }

        public override Task HandleBasicDeliver(
            string consumerTag,
            ulong deliveryTag,
            bool redelivered,
            string exchange,
            string routingKey,
            IBasicProperties properties,
            ReadOnlyMemory<byte> body)
        {
            var span = body.Span;
            var message = Encoding.UTF8.GetString(span);
            return base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);
        }

        public override Task HandleBasicConsumeOk(string consumerTag)
        {
            return base.HandleBasicConsumeOk(consumerTag);
        }

        public override Task OnCancel(params string[] consumerTags)
        {
            return base.OnCancel(consumerTags);
        }

        public override Task HandleModelShutdown(object model, ShutdownEventArgs reason)
        {
            return base.HandleModelShutdown(model, reason);
        }
    }
}