﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class Section
    {
        public Section()
        {
            Machines = new List<Machine>();
            SectionGroups = new List<SectionGroup>();
            Products = new List<Product>();
        }

        public int SectionId { get; set; }

        public string SectionName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public ICollection<Machine> Machines { get; set; }

        public ICollection<SectionGroup> SectionGroups { get; set; }

        public ICollection<Product> Products { get; set; }
        
    }
}
