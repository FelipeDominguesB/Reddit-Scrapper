﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Worker
{
    public interface IWorkerService
    {
        public Task Start();

    }
}
