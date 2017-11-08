using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmbeddedStock2.Models;
using Microsoft.AspNetCore.Http;

namespace EmbeddedStock2.ViewModels
{
    public class CreateComponentTypeViewModel
    {
        public string ComponentName { get; set; }
        public string ComponentInfo { get; set; }
        public string Location { get; set; }
        public ComponentTypeStatus Status { get; set; }
        public string Datasheet { get; set; }
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }
        public string WikiLink { get; set; }
        public string AdminComment { get; set; }
        public IFormFile ImageUpload { get; set; }
        public int Category { get; set; }
    }
}
