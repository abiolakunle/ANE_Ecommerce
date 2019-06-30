using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class MachineSectionGroup
    {
        public int MachineSectionGroupId { get; set; }

        public Machine Machine { get; set; }
        public int MachineId { get; set; }

        public SectionGroup SectionGroup { get; set; }
        public int SectionGroupId { get; set; }

    }
}
