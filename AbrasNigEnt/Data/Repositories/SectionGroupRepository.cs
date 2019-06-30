using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrasNigEnt.Data.Repositories
{
    public class SectionGroupRepository : Repository<SectionGroup>, ISectionGroupRepository
    {
        public SectionGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
                
        }
    }
}
