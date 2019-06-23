﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class Machine
    {
        public int MachineId { get; set; }

        public string ModelName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string SerialNumber { get; set; }

        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public MachineType MachineType { get; set; }

        public ICollection<Section> Sections { get; set; }

        public ICollection<SectionGroup> SectionGroups { get; set; }
    }
}
