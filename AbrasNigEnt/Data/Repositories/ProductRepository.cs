using AbrasNigEnt.Data.Interfaces;
using AbrasNigEnt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AbrasNigEnt.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> FindWithCategoryAndBrand(Func<Product, bool> predicate)
        {
            return _context.Products.Include(p => p.Category).Include(p => p.Brand).Where(predicate);
        }

        public IEnumerable<Product> LoadAllWithCategoryAndBrand()
        {
            return _context.Products.Include(p => p.Category).Include(p => p.Brand);
        }
    }
}
