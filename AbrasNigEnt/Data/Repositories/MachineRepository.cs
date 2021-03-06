﻿using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Repositories
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        public MachineRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Machine> LoadAllWithBrand()
        {
            return _context.Machines.Include(m => m.Brand);
        }

        public Machine LoadWithBrandAndSection(Machine machine)
        {
            return _context.Machines
                .Where(m => m.MachineId == machine.MachineId)
                .Include(m => m.Brand).Include(m => m.Sections)
                .FirstOrDefault();
        }
    }
}
