using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock2.Models
{
    public class ComponentTypeCategory
    {
        public long ComponentTypeId { get; set; }
        public ComponentType ComponentType { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
