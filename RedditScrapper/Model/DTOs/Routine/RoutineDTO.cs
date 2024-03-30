using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.DTOs.Routine
{
    public class RoutineDTO
    {
        public string SubredditName { get; set; }
        public int SyncRate { get; set; }
        public int MaxPostsPerSync { get; set; }
        public int PostSorting { get; set; }
        public DateTime? NextRun { get; set; }
    }
}
