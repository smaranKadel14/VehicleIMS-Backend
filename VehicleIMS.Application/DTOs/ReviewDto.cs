using System.ComponentModel.DataAnnotations;

namespace VehicleIMS.Application.DTOs
{
    public class ReviewDto
    {
        [Required, Range(1, 5)] public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int? PartId { get; set; }
    }
}