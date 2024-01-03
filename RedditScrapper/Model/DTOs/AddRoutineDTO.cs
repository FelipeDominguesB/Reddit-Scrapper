using RedditScrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.DTOs
{
    public class AddRoutineDTO
    {
        public string SubredditName { get; set; }
        public RateEnum SyncRate { get; set; }
        public SortingEnum PostSorting { get; set; }
        public int MaxPostsPerSync { get; set; }
        public bool RunImmediatly { get; set; }
    }
}
