using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Domain.Entities
{
    public class SyncRoutine : BaseEntity
    {        
        public string SubredditName { get; set; }
        public int SyncRate { get; set; }
        public int MaxPostsPerSync { get; set; }
        public int PostSorting { get; set; }
        public DateTime? NextRun { get; set; }
        public ICollection<SyncHistory> SyncHistories { get; set; }

    }
}
