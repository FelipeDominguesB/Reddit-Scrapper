using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Domain.Entities
{
    public class RoutineExecution : BaseEntity
    {
        public long RoutineId { get; set; }
        public bool Succeded { get; set; }
        public int MaxPostsPerSync { get; set; }
        public int SyncRate { get; set; }
        public int PostSorting { get; set; }
        public int TotalLinksFound { get; set; }

        public ICollection<RoutineExecutionFile> RoutineExecutionFiles { get; set; }

        [ForeignKey("RoutineId")]
        public virtual Routine Routine { get; set; }

    }
}
