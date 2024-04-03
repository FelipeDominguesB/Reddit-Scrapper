using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Configuration
{
    public class RabbitMQConfiguration
    {
        public RabbitMQConfiguration() { }

        public string HostName { get; set; }
        public string QueueName { get; set; }

    }
}
