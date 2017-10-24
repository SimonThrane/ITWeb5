using Microsoft.EntityFrameworkCore;

namespace EmbeddedStock2.Models
{
    public class EmbeddedStock2Context: DbContext
    {
        public EmbeddedStock2Context(DbContextOptions<EmbeddedStock2Context> options)
            :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<EsImage> EsImages { get; set; }
        public DbSet<ComponentTypeCategory> ComponentTypeCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Component>().ToTable("Component");
            modelBuilder.Entity<ComponentType>().ToTable("ComponentType");
            modelBuilder.Entity<EsImage>().ToTable("EsImage");
            modelBuilder.Entity<ComponentTypeCategory>().ToTable("ComponentTypeCategory");

            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<Component>()
                .HasKey(c => c.ComponentId);

            modelBuilder.Entity<ComponentType>()
                .HasKey(ct => ct.ComponentTypeId);

            modelBuilder.Entity<EsImage>()
                .HasKey(ei => ei.ESImageId);

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasKey(cc => new { cc.CategoryId, cc.ComponentTypeId });

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.ComponentTypeCategory)
                .HasForeignKey(cc => cc.CategoryId);

            modelBuilder.Entity<ComponentTypeCategory>()
                .HasOne(cc => cc.ComponentType)
                .WithMany(ct => ct.ComponentTypeCategory)
                .HasForeignKey(cc => cc.ComponentTypeId);

        }
    }
}
