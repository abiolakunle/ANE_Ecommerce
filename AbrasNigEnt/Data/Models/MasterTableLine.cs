using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Models
{
    public class MasterTableLine
    {
        public string ModelName { get; set; }

        public string SerialNumber { get; set; }

        public string Section { get; set; }

        public string SectionGroup { get; set; }

        public string PartName { get; set; }

        public string PartNumber { get; set; }

        public int Quantity { get; set; }

        public string Remarks { get; set; }

        public string Brand { get; set; }

    }
}
