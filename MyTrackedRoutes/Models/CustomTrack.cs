using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MyTrackedRoutes.Models;

public class CustomTrack
{

    [Key]
    public int Id { get; set; }
    public List<Location> Locations { get; set; } = new();

    public CustomTrack()
    {        
    }

    public string LocationsJson
    {
        get => JsonSerializer.Serialize(Locations);
        set => Locations = string.IsNullOrEmpty(value) ? new List<Location>() : JsonSerializer.Deserialize<List<Location>>(value);
    }

    public CustomTrack(IList<Location> locations)
    {
        Locations = locations.ToList();
    }
}