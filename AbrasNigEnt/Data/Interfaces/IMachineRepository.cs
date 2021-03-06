﻿using AbrasNigEnt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Interfaces
{
    public interface IMachineRepository : IRepository<Machine>
    {
        IEnumerable<Machine> LoadAllWithBrand();
        Machine LoadWithBrandAndSection(Machine machine);
    }
}
