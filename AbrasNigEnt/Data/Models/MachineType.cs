using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class MachineType
    {
        public int MachineTypeId { get; set; }

        public string MachineTypeName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Machine> Machines { get; set; }
    }
}
