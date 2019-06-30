using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class SectionGroup
    {
        public SectionGroup()
        {
            Products = new HashSet<Product>();
            //Machines = new HashSet<Machine>();
        }

        public int SectionGroupId { get; set; }

        public string SectionGroupName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public virtual Section Section { get; set; }
        public int SectionId { get; set; }

        //public Machine Machine { get; set; }
        //public int MachineId { get; set; }

        //public virtual ICollection<Machine> Machines { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
