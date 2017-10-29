using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FreeImageAPI;

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
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), @".\seedImages\pump.jpg");
                var esImage = new EsImage
                {
                    ImageMimeType = "image/jpeg",
                    ImageData = Util.Util.ImageToByteArray(imagePath, FREE_IMAGE_FORMAT.FIF_JPEG),
                    Thumbnail = Util.Util.ThumbNailByteArray(imagePath, FREE_IMAGE_FORMAT.FIF_JPEG)
                };

                var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), @"..\seedImages\pump2.jpg");
                var esImage2 = new EsImage
                {
                    ImageMimeType = "image/jpeg",
                    ImageData = Util.Util.ImageToByteArray(imagePath, FREE_IMAGE_FORMAT.FIF_JPEG),
                    Thumbnail = Util.Util.ThumbNailByteArray(imagePath, FREE_IMAGE_FORMAT.FIF_JPEG)
                };

                var componentTypes = new[]
                {
                    new ComponentType
                    {
                        ComponentName = "Pumpe",
                        AdminComment = "",
                        ComponentInfo = "",
                        Datasheet = "Datasheet",
                        Status = ComponentTypeStatus.Available,
                        Location = "Kontoret",
                        Manufacturer = "Skolen",
                        WikiLink = "https://da.wikipedia.org/wiki/Pumpe",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/Drehkolbenpumpe.jpg/220px-Drehkolbenpumpe.jpg",
                        Image = esImage
                    },
                    new ComponentType
                    {
                        ComponentName = "Pumpe2",
                        AdminComment = "",
                        ComponentInfo = "",
                        Datasheet = "Datasheet",
                        Status = ComponentTypeStatus.Available,
                        Location = "Kontoret",
                        Manufacturer = "Skolen",
                        WikiLink = "https://da.wikipedia.org/wiki/Pumpe",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/Drehkolbenpumpe.jpg/220px-Drehkolbenpumpe.jpg",
                        Image = esImage2
                    }
                };

                ComponentTypes.AddRange(componentTypes);
                SaveChanges();


                var components = new Component[]
                {
                    new Component
                    {
                        AdminComment = "Pumpe",
                        ComponentNumber = 1,
                        SerialNo = "123456789",
                        Status = ComponentStatus.Available,
                        UserComment = "Lånt i 1 dag",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Pumpe",
                        ComponentNumber = 2,
                        SerialNo = "234567891",
                        Status = ComponentStatus.ReservedAdmin,
                        UserComment = "Lånt i 2 dage",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Pumpe",
                        ComponentNumber = 3,
                        SerialNo = "345678912",
                        Status = ComponentStatus.Available,
                        UserComment = "Lånt i 3 dage",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Pumpe",
                        ComponentNumber = 4,
                        SerialNo = "456789123",
                        Status = ComponentStatus.Defect,
                        UserComment = "I stykker",
                        CurrentLoanInformationId = null
                    },
                    new Component
                    {
                        AdminComment = "Pumpe",
                        ComponentNumber = 5,
                        SerialNo = "567891234",
                        Status = ComponentStatus.Available,
                        UserComment = "Lånt i 5 dage",
                        CurrentLoanInformationId = null
                    }
                };

                var currentComponentType = ComponentTypes.FirstOrDefault();
                foreach (var component in components)
                {
                    currentComponentType.Components.Add(component);
                }
                SaveChanges();

                var categories = new[]
                {
                    new Category
                    {
                        Name = "Motordrevet"
                    },
                    new Category
                    {
                        Name = "Elektronik"
                    }
                };

                Categories.AddRange(categories);
                SaveChanges();

                var componentTypeCategoies = new[]
                {
                    new ComponentTypeCategory
                    {
                        ComponentType = ComponentTypes.FirstOrDefault(),
                        Category = Categories.FirstOrDefault()
                    }
                };

                ComponentTypeCategory.AddRange(componentTypeCategoies);
                SaveChanges();
            }
        }
    }
}
