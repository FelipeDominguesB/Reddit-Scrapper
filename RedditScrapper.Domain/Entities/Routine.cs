using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Domain.Entities
{
    public class Routine : BaseEntity
    {        
        public string SubredditName { get; set; }
        public int SyncRate { get; set; }
        public int MaxPostsPerSync { get; set; }
        public int PostSorting { get; set; }
        public DateTime? NextRun { get; set; }
        public ICollection<RoutineExecution> RoutineHistory { get; set; }

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
