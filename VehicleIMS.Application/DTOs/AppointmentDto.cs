using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class AppointmentDto
    {
        [Required] public DateTime AppointmentDate { get; set; }
        [Required] public int VehicleId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}