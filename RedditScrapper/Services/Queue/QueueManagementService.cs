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
using Microsoft.Extensions.Configuration;
using RedditScrapper.Configuration;
using Microsoft.Extensions.Logging;
using RedditScrapper.Model.Message;

namespace RedditScrapper.Services.Queue
{
    public abstract class QueueManagementService<TItem> : IQueueService<TItem> where TItem : class
    {
        private readonly ILogger<QueueManagementService<RedditPostMessage>> _logger;
        private readonly RabbitMQConfiguration rabbitMQConfiguration;        
        public QueueManagementService(IConfiguration configuration, ILogger<QueueManagementService<RedditPostMessage>> logger) 
        {
            _logger = logger;
            rabbitMQConfiguration = configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();
        }

        public void Publish(TItem item)
        {

            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = rabbitMQConfiguration.HostName };
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();


            channel.QueueDeclare(queue: rabbitMQConfiguration.QueueName,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            string itemBody = JsonConvert.SerializeObject(item);

            byte[] body = Encoding.UTF8.GetBytes(itemBody);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty, routingKey: rabbitMQConfiguration.QueueName, basicProperties: properties, body: body);
        }

        public void Read()
        {
            _logger.LogInformation("Starting to read queue");

            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = rabbitMQConfiguration.HostName };
            connectionFactory.DispatchConsumersAsync = true;

            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();


            channel.QueueDeclare(queue: rabbitMQConfiguration.QueueName,
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

            channel.BasicConsume(queue: rabbitMQConfiguration.QueueName, autoAck: false, consumer: consumer);

            _logger.LogInformation("Queue consume has started");

        }


        protected abstract Task<bool> HandleValue(TItem item);
    }
}
