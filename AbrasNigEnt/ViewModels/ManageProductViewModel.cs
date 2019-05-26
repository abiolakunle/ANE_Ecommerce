using AbrasNigEnt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.ViewModels
{
    public class ManageProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<Brand> Brands { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
