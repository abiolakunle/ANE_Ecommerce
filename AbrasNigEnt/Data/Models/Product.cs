

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

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }

        public string Remarks { get; set; }

        public SectionGroup SectionGroup { get; set; }

        public Section Section { get; set; }       
    }
}
