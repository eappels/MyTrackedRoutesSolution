using MyTrackedRoutes.Services.Interfaces;

namespace MyTrackedRoutes.Services;

public partial class LocationService : ILocationService
{
    public Action<Location> OnLocationUpdate { get; set; }

    public void StartTracking()
    {
        StartTrackingInternal();
    }

    public void StopTracking()
    {
        StopTrackingInternal();
    }

    partial void StartTrackingInternal();
    partial void StopTrackingInternal();
}