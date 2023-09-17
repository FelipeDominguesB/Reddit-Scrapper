using RedditScrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model
{
    public class AddRoutineDTO
    {
        public string SubredditName { get; set; }
        public SortingEnum Sorting { get; set; }
    }
}
