using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.DTOs.Health
{
    public class PendingMigrationsDTO
    {

        public PendingMigrationsDTO() { }

        public ICollection<string> PendingMigrations { get; set; }

        public int PendingMigrationsCount { get; set; }

        public bool IsPendingMigrations { get { return PendingMigrationsCount > 0; } }
    }
}
