using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Maps;
using MyTrackedRoutes.Messages;
using MyTrackedRoutes.ViewModels;

namespace MyTrackedRoutes.Views;

public partial class MapView : ContentPage
{

    private double zoomLevel = 100;

    public MapView()
	{
		InitializeComponent();

        WeakReferenceMessenger.Default.Register<LocationUpdateMessage>(this, (r, m) =>
        {
            if (MyMap != null && m.Value != null)
            {
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(m.Value, Distance.FromMeters(zoomLevel)));
            }
        });

        if (MyMap != null)
        {
            MyMap.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "VisibleRegion")
                {
                    zoomLevel = MyMap.VisibleRegion.Radius.Meters;
                }
            };
        }
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