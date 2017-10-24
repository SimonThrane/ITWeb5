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

    }
}
