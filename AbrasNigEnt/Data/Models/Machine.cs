using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class Machine
    {
        public Machine()
        {
            Sections = new List<Section>();
            SectionGroups = new List<SectionGroup>();
        }

        public int MachineId { get; set; }

        public string ModelName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string SerialNumber { get; set; }

        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }

        public MachineType MachineType { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<SectionGroup> SectionGroups { get; set; }
    }
}
