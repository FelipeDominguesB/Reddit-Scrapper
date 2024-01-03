using RabbitMQ.Client;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Diagnostics;

namespace RedditScrapper.Services.Queue
{
    public abstract class QueueManagementService<TItem> : IQueueService<TItem> where TItem : class
    {
        public void Publish(TItem item)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();


            channel.QueueDeclare(queue: "item_queue",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            string itemBody = JsonConvert.SerializeObject(item);

            byte[] body = Encoding.UTF8.GetBytes(itemBody);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty, routingKey: "item_queue", basicProperties: properties, body: body);
        }

        public void Read()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            connectionFactory.DispatchConsumersAsync = true;

            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();


            channel.QueueDeclare(queue: "item_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string itemBody = Encoding.UTF8.GetString(body);

                TItem item = JsonConvert.DeserializeObject<TItem>(itemBody);


                await HandleValue(item!);


                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); ;

            };

            channel.BasicConsume(queue: "item_queue", autoAck: false, consumer: consumer);
        }


        protected abstract Task<bool> HandleValue(TItem item);
    }
}
