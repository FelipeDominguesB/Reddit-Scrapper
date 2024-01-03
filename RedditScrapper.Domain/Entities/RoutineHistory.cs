﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Domain.Entities
{
    public class RoutineHistory : BaseEntity
    {
        public long RoutineId { get; set; }

        public bool Succeded { get; set; }

        [ForeignKey("RoutineId")]
        public virtual Routine Routine { get; set; }

    }
}