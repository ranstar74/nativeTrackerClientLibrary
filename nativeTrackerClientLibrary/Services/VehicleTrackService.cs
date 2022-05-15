using Grpc.Core;
using nativeTrackerClientService;

namespace nativeTrackerClientLibrary.Services;

public class VehicleTrackService : ProtoServiceBase
{
    private readonly nativeTrackerClientService.VehicleTrackService.VehicleTrackServiceClient _trackService;

    public VehicleTrackService()
    {
        _trackService = new nativeTrackerClientService.VehicleTrackService.VehicleTrackServiceClient(Channel);
    }

    public IAsyncEnumerable<VehicleTrackUpdate> TrackVehicle(int vehicleHandle)
    {
        var request = new VehicleTrackRequest()
        {
            VehicleHandle = vehicleHandle
        };
        using var response = _trackService.Subscribe(request);
        return response.ResponseStream.ReadAllAsync();
    }
}