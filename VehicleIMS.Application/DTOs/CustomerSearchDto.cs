namespace VehicleIMS.Application.Customers.DTOs;

public class CustomerSearchDto
{
    public string? Query { get; set; }
    public string SearchType { get; set; } = "All";  // All | Name | Phone | Vehicle | ID
    public string Status { get; set; } = "All";      // All | Active | Inactive
    public string SortBy { get; set; } = "Name";     // Name | Total Spend | Last Visit | Vehicles
}