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
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
