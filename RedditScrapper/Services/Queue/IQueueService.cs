using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Queue
{
    public interface IQueueService<TItem> where TItem : class
    {
        public void Publish(TItem item);

        public void Read();

    }
}
