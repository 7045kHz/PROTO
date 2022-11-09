﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTO.Core.Models
{
    public class BasicAuthorization
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
