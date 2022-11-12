﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTO.Core.Models
{
    public class ProjectSchedule
    {
        public int Id { get; set; }
        public string Project { get; set; } = String.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string OwnedBy { get; set; } = String.Empty;

    }
}
