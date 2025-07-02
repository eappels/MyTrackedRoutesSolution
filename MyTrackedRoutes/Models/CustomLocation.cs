using System.ComponentModel.DataAnnotations;

namespace MyTrackedRoutes.Models;

public class CustomLocation
{
    [Key]
    public DateTime Timestamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public CustomLocation(double latitude, double longitude)
    {
        Timestamp = DateTime.Now;
        Latitude = latitude;
        Longitude = longitude;
    }
}