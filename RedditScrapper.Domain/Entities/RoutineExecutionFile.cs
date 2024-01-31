using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Domain.Entities
{
    public class RoutineExecutionFile : BaseEntity
    {
        public RoutineExecutionFile() { }

        public long ExecutionId { get; set; }
        public string? FileName { get; set; }
        public string SourceUrl { get; set; }
        public string? DownloadDirectory { get; set; }
        public int? Classification { get; set; }
        public bool Succeded { get; set; }

        [ForeignKey("ExecutionId")]
        public virtual RoutineExecution RoutineExecution { get; set; }
    }
}
