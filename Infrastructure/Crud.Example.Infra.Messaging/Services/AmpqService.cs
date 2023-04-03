using System.Text;
using Crud.Example.Infrastructure.Messaging.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Crud.Example.Infrastructure.Messaging.Services
{
    public class AmpqService
    {
        private readonly ConnectionFactory? _connectionFactory;
        private IConnection? _connection;
        private IModel? _channel;

        public AmpqService(IOptions<AmpqInfo> ampOptionsSnapshot)
        {
            AmpqInfo amqpInfo = ampOptionsSnapshot.Value;

            _connectionFactory = new ConnectionFactory
            {
                UserName = amqpInfo.Username,
                Password = amqpInfo.Password,
                HostName = amqpInfo.HostName,
            };
        }

        /// <summary>
        /// Publish a message in RabbitMQ.
        /// </summary>
        /// <param name="message"></param>
        public void PublishMessage(string message,
            string exchangeName = "",
            string queueName = "")
        {
            using (_connection = _connectionFactory!.CreateConnection())
            using (_channel = _connection.CreateModel())
            {
                _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                CreateQueue(_channel, queueName);
                CreateExchange(_channel, exchangeName);
                BindExchangeQueue(_channel, queueName, exchangeName);
    
                var properties = _channel.CreateBasicProperties();

                byte[] messagebuffer = Encoding.Default.GetBytes(message);

                properties.Persistent = true;
                _channel.BasicPublish(exchange: exchangeName,
                    routingKey: "directexchange_key",
                    basicProperties: properties,
                    body: messagebuffer
                );
            }
        }

        private static void CreateExchange(IModel channel, string exchangeName)
        {
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        }

        private static void CreateQueue(IModel channel, string queueName)
        {
            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        private static void BindExchangeQueue(IModel channel, string queueName, string exchangeName)
        {
            channel.QueueBind(queueName, exchangeName, "directexchange_key");
        }
    }
}