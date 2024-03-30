using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.DTOs.Routine
{
    public class RoutineExecutionDTO : BaseEntityDTO
    {
        public RoutineExecutionDTO() { }

        public long RoutineId { get; set; }
        public bool Succeded { get; set; }
        public int MaxPostsPerSync { get; set; }
        public int SyncRate { get; set; }
        public int PostSorting { get; set; }
        public int TotalLinksFound { get; set; }


    }
}
