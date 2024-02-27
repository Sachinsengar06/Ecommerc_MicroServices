using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqManager.Service
{
    public class RabbitMq:IRabbitMq
    {
        private readonly IConnection _connection;

        public RabbitMq(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 15672,
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
        }

        public void PublishMessage<T>(string queueName, T message)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "", queueName, basicProperties: null, body: body);
            }
        }

        public void ConsumeMessages<T>(string queueName, Action<T> onMessageReceived)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));
                    onMessageReceived(message);
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
        }
    }
}
