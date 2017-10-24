using System;
using System.Collections.Generic;

namespace Model
{
    public class Category
    {
        public Category()
        {
            ComponentTypes = new List<ComponentType>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ComponentType> ComponentTypes { get; protected set; }

        public class ComponentType
        {
            public ComponentType()
            {
                Components = new List<Component>();
                    . Categories = new List<Category>();
            }

            public long ComponentTypeId { get; set; }
            public string ComponentName { get; set; }
            public string ComponentInfo { get; set; }
            public string Location { get; set; }
            public ComponentTypeStatus Status { get; set; }
            public string Datasheet { get; set; }
            public string ImageUrl { get; set; }
            public string Manufacturer { get; set; }
            public string WikiLink { get; set; }
            public string AdminComment { get; set; }
            public virtual ESImage Image { get; set; }
            public ICollection<Component> Components { get; protected set; }
            public ICollection<Category> Categories { get; protected set; }
        }

        public class Component
        {
            public long ComponentId { get; set; }
            public long ComponentTypeId { get; set; }
            public int ComponentNumber { get; set; }
            public string SerialNo { get; set; }
            public ComponentStatus Status { get; set; }
            public string AdminComment { get; set; }
            public string UserComment { get; set; }
            public long? CurrentLoanInformationId { get; set; }
        }

        public class ESImage
        {
            public long ESImageId { get; set; }

            [MaxLength(128)]
            public string ImageMimeType { get; set; }

            public byte[] Thumbnail { get; set; }
            public byte[] ImageData { get; set; }
        }

        public enum ComponentTypeStatus
        {
            Available,
            ReservedAdmin
        }

        public enum ComponentStatus
        {
            Available,
            ReservedLoaner,
            ReservedAdmin,
            Loaned,
            Defect,
            Trashed,
            Lost,
            NeverReturned
        }
    }
}
