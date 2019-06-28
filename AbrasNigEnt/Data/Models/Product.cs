

using System.Collections;
using System.Collections.Generic;

namespace AbrasNigEnt.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string PartNumber { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Detail { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string Quantity { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public string Remarks { get; set; }

        public ICollection<Section> Sections { get; set; }

        public virtual SectionGroup SectionGroup { get; set; }

        public virtual Section Section { get; set; }       
    }
}
