using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PROTO.Core.Models
{
    public class HostDevice
    {
        public int Id { get; set; }
        public string Hostname { get; set; } = String.Empty;
        public string Domain { get; set; } = String.Empty;

        public string SerialNumber { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; }

    }
}
