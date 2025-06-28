using CoreLocation;

namespace MyTrackedRoutes.Services;

public partial class LocationService : IDisposable
{

    public readonly CLLocationManager locationManager;

    public LocationService()
    {
        locationManager = new CLLocationManager();
        locationManager.PausesLocationUpdatesAutomatically = false;
        locationManager.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
        locationManager.AllowsBackgroundLocationUpdates = true;
        locationManager.ActivityType = CLActivityType.AutomotiveNavigation;
        locationManager.LocationsUpdated += OnLocationsUpdated;
    }

    private void OnLocationsUpdated(object? sender, CLLocationsUpdatedEventArgs e)
    {
        var lastLocation = e.Locations.LastOrDefault();
        if (lastLocation != null)
        {
            OnLocationUpdate?.Invoke(new Location(
                lastLocation.Coordinate.Latitude,
                lastLocation.Coordinate.Longitude
            ));
        }
    }

    partial void StartTrackingInternal()
    {
        locationManager.StartUpdatingLocation();
    }

    partial void StopTrackingInternal()
    {
        locationManager.StopUpdatingLocation();
    }

    public void Dispose()
    {
        locationManager.LocationsUpdated -= OnLocationsUpdated;
        locationManager.Dispose();
        GC.SuppressFinalize(this);
    }

}