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

        public DbSet<SectionGroup> SectionGroups { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Machine>().HasMany(m => m.Sections);
            modelBuilder.Entity<Machine>().HasMany(m => m.SectionGroups);

            modelBuilder.Entity<Section>().HasMany(s => s.Machines);

            modelBuilder.Entity<SectionGroup>().HasMany(s => s.Machines);

            modelBuilder.Entity<Product>().HasOne(s => s.SectionGroup);
        }
    }
}
