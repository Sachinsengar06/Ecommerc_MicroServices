using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqManager.Service
{
    public interface IRabbitMq
    {
        void PublishMessage<T>(string queueName, T message);
        void ConsumeMessages<T>(string queueName,  Action<T>onMessageReceived);
    }
}
