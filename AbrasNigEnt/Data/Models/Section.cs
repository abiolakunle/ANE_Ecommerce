using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class Section
    {
        public Section()
        {
            //Machines = new HashSet<Machine>();
            SectionGroups = new HashSet<SectionGroup>();
            //Products = new HashSet<Product>();
        }

        public int SectionId { get; set; }

        public string SectionName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        //public virtual ICollection<Machine> Machines { get; set; }

        public virtual ICollection<SectionGroup> SectionGroups { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
        
    }
}
