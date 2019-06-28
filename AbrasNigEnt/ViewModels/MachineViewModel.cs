using AbrasNigEnt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.ViewModels
{
    public class MachineViewModel
    {
        public Machine Machine { get; set; }

        public IEnumerable<Section> Sections { get; set; }

        public IEnumerable<SectionGroup> SectionGroups { get; set; }        
    }
}
