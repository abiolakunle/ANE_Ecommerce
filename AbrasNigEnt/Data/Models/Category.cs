﻿using System.Collections.Generic;


namespace AbrasNigEnt.Data.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
