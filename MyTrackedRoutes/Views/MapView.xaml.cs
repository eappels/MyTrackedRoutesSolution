using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using MyTrackedRoutes.ViewModels;

namespace MyTrackedRoutes.Views;

public partial class MapView : ContentPage
{
	public MapView()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (MyMap != null)
            MyMap.MapElements.Add(((MapViewModel)BindingContext).Track);

        Location currentLocation = await GetCachedLocation();

        MapSpan mapSpan = MapSpan.FromCenterAndRadius(currentLocation, Distance.FromMeters(250));
        MyMap.MoveToRegion(mapSpan);
    }

    public async Task<Location> GetCachedLocation()
    {
        try
        {
            Location location = await Geolocation.Default.GetLastKnownLocationAsync();

            if (location != null)
                return location;
        }
        catch { }

        return new Location();
    }
}