using System.ComponentModel.DataAnnotations;

namespace EmbeddedStock2.Models
{
    public class Component
    {
        [Display(Name = "Id")]
        public long ComponentId { get; set; }
        [Display (Name = "Type")]
        public long ComponentTypeId { get; set; }
        public int ComponentNumber { get; set; }
        public string SerialNo { get; set; }
        public ComponentStatus Status { get; set; }
        [Display(Name = "Comment")]
        public string AdminComment { get; set; }
        public string UserComment { get; set; }
        [Display(Name = "Loaner ID")]
        public long? CurrentLoanInformationId { get; set; }
    }
}