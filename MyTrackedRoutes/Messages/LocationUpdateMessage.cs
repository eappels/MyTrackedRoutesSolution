using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MyTrackedRoutes.Messages;

public class LocationUpdateMessage : ValueChangedMessage<Location>
{
    public LocationUpdateMessage(Location location)
        : base(location)
    {
    }
}