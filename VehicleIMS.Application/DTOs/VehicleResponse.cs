namespace VehicleIMS.Application.Customers.DTOs;

public class VehicleResponse
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string VIN { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
}
