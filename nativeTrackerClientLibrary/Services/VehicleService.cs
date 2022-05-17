using Grpc.Core;
using nativeTrackerClientService;

namespace nativeTrackerClientLibrary.Services;

public class VehicleService : ProtoServiceBase
{
    private readonly nativeTrackerClientService.VehicleService.VehicleServiceClient _client;

    public VehicleService()
    {
        _client = new nativeTrackerClientService.VehicleService.VehicleServiceClient(Channel);
    }

    public IAsyncEnumerable<GetVehiclesResponse> GetVehicles()
    {
        var response = _client.GetVehicles(
            new GetVehiclesRequest());
        return response.ResponseStream.ReadAllAsync();
    }
    
    public async Task AddVehicle(AddVehicleRequest request)
    {
        var response = await _client.AddVehicleAsync(request);
    }
}