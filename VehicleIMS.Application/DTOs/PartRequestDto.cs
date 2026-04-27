using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class PartRequestDto
    {
        [Required] public string PartName { get; set; } = string.Empty;
        public int? PartId { get; set; }  
    }
}