using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class VehicleDto
    {
        [Required] public string Make { get; set; } = string.Empty;
        [Required] public string Model { get; set; } = string.Empty;
        [Required] public int Year { get; set; }
        [Required] public string VIN { get; set; } = string.Empty;
        [Required] public string LicensePlate { get; set; } = string.Empty;
    }
}