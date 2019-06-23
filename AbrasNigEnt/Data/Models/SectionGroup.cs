using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class SectionGroup
    {
        public int SectionGroupId { get; set; }

        public string SectionGroupName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public Section Section { get; set; }
        public int SectionId { get; set; }

        public Machine Machine { get; set; }
        public int MachineId { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
