using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class MachineSection
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MachineSectionId { get; set; }

        public int MachineId { get; set; }
        public Machine Machine { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
