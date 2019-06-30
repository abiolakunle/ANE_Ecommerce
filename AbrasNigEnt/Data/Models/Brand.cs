using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
            Categories = new HashSet<Category>();
            Machines = new HashSet<Machine>();
            Sections = new HashSet<Section>();
            SectionGroups = new HashSet<SectionGroup>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Machine> Machines { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<SectionGroup> SectionGroups { get; set; }
    }
}
