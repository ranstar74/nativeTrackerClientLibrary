using Grpc.Core;
using nativeTrackerClientService;

namespace nativeTrackerClientLibrary;

public struct VehicleState
{
    public int VehicleHandle { get; }
    public double Longitude { get; }
    public double Latitude { get; }
    public DateTime Time { get; }

    public VehicleState(int vehicleHandle, double longitude, double latitude, DateTime time)
    {
        VehicleHandle = vehicleHandle;
        Longitude = longitude;
        Latitude = latitude;
        Time = time;
    }
}

public class VehicleTracker : ProtoServiceBase
{
    private readonly VehicleTrackService.VehicleTrackServiceClient _trackService;

    public VehicleTracker()
    {
        _trackService = new VehicleTrackService.VehicleTrackServiceClient(Channel);
    }

    public async void TrackVehicle(int vehicleHandle, Action<VehicleState> onUpdate)
    {
        var request = new VehicleTrackRequest()
        {
            VehicleHandle = vehicleHandle
        };
        using var stream = _trackService.Subscribe(request);

        await foreach (var update in stream.ResponseStream.ReadAllAsync())
        {
            onUpdate(new VehicleState(
                vehicleHandle,
                update.Longitude,
                update.Latitude,
                update.Time.ToDateTime()));
        }
    }
}