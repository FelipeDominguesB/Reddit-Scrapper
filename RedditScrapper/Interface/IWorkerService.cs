using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Interface
{
    public interface IWorkerService
    {
        public Task<bool> Start();
        public Task<bool> Stop();

    }
}
