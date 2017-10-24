using System.Collections.Generic;

namespace EmbeddedStock2.Models
{
    public class Category
    {
        public Category()
        {
            ComponentTypeCategory = new List<ComponentTypeCategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ComponentTypeCategory> ComponentTypeCategory { get; protected set; }
    }
}
