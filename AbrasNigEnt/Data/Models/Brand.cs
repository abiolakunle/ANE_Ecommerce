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
            Products = new List<Product>();
            Categories = new List<Category>();
            Machines = new List<Machine>();
            Sections = new List<Section>();
            SectionGroups = new List<SectionGroup>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public ICollection<Product> Products { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Machine> Machines { get; set; }

        public ICollection<Section> Sections { get; set; }

        public ICollection<SectionGroup> SectionGroups { get; set; }
    }
}
