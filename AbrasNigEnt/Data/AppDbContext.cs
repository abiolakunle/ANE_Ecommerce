using AbrasNigEnt.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AbrasNigEnt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<MachineType> MachineTypes { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<SectionGroup> SectionTypes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Machine>().HasMany(m => m.Sections);
            modelBuilder.Entity<Section>().HasMany(s => s.Machines);

            
        }
    }
}
