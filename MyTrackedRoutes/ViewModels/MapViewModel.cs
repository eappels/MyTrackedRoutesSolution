using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using MyTrackedRoutes.Helpers;
using MyTrackedRoutes.Messages;
using MyTrackedRoutes.Models;
using MyTrackedRoutes.Services.Interfaces;

namespace MyTrackedRoutes.ViewModels;

public partial class MapViewModel : ObservableObject, IDisposable
{

    private readonly ILocationService locationService;
    private AppDbContext dbContext;

    public MapViewModel(ILocationService locationService)
    {
        dbContext = new AppDbContext();
        dbContext.Database.EnsureCreated();
        StartStopButtonEnabed = true;
        StartStopButtonColor = "Green";
        StartStopButtonText = "Start";
        this.locationService = locationService;
        locationService.OnLocationUpdate = OnLocationUpdate;

        Track = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };

        var tracks = dbContext.Tracks.LastOrDefault()?.Locations ?? new List<Location>();
        if (tracks.Any())
        {
            foreach (var location in tracks)
            {
                Track.Geopath.Add(location);
            }
        }
    }

    private void OnLocationUpdate(Location location)
    {
        Track.Geopath.Add(location);
        WeakReferenceMessenger.Default.Send(new LocationUpdateMessage(location));
    }

    public void Dispose()
    {
        if (locationService != null)
        {
            locationService.OnLocationUpdate = null;
            locationService.StopTracking();
        }
    }

    [RelayCommand]
    private async Task StartStopTracking()
    {
        StartStopButtonText = StartStopButtonText == "Start" ? "Stop" : "Start";

        if (StartStopButtonText == "Stop")
        {
            Track.Geopath.Clear();
            locationService.StartTracking();
            StartStopButtonColor = "Red";
        }
        else
        {
            locationService.StopTracking();
            StartStopButtonEnabed = false;
            var result = await Application.Current.MainPage.DisplayAlert("Save Track", "Do you want to save the current track?", "Yes", "No");
            if (result == true)
            {
                var track = new CustomTrack(Track.Geopath);
                dbContext.Tracks.Add(track);
                dbContext.SaveChanges();
            }
        }
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private string startStopButtonText;

    [ObservableProperty]
    private string startStopButtonColor;

    [ObservableProperty]
    private bool startStopButtonEnabed;
}