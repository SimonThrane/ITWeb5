using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public void MigrateAndSeedData()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }

            if(!Components.Any())
            {
                //Seed database here
                var components = new Component[]
                {
                    new Component
                    {
                        AdminComment = "Første component",
                        ComponentNumber = 1,
                        SerialNo = "123456789",
                        Status = ComponentStatus.Available,
                        UserComment = "Lånt i 1 dag",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Anden component",
                        ComponentNumber = 2,
                        SerialNo = "234567891",
                        Status = ComponentStatus.ReservedAdmin,
                        UserComment = "Lånt i 2 dage",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Tredje component",
                        ComponentNumber = 3,
                        SerialNo = "345678912",
                        Status = ComponentStatus.Available,
                        UserComment = "Lånt i 3 dage",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Fjerde component",
                        ComponentNumber = 4,
                        SerialNo = "456789123",
                        Status = ComponentStatus.Defect,
                        UserComment = "I stykker",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Femte component",
                        ComponentNumber = 5,
                        SerialNo = "567891234",
                        Status = ComponentStatus.Available,
                        UserComment = "Lånt i 5 dage",
                        CurrentLoanInformationId = null
                    }
                };
            }
        }
    }
}
