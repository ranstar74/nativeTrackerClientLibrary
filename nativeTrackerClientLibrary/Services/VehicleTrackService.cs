using Grpc.Core;
using nativeTrackerClientService;

namespace nativeTrackerClientLibrary.Services;

public class VehicleTrackService : ProtoServiceBase
{
    private readonly nativeTrackerClientService.VehicleTrackService.VehicleTrackServiceClient _client;

    public VehicleTrackService()
    {
        _client = new nativeTrackerClientService.VehicleTrackService.VehicleTrackServiceClient(Channel);
    }

    public IAsyncEnumerable<VehicleTrackUpdate> TrackVehicle(int vehicleHandle)
    {
        var request = new VehicleTrackRequest()
        {
            VehicleHandle = vehicleHandle
        };
        using var response = _client.Subscribe(request);
        return response.ResponseStream.ReadAllAsync();
    }

    public async Task<IEnumerable<string>> GetTrackModelManufacturers()
    {
        var result = await _client.GetTrackModelsManufacturersAsync(
            new GetTrackModelManufacturersRequest());

        return result.Manufacturers;
    }

    public async Task<IEnumerable<string>> GetTrackModelsFeatures()
    {
        var result = await _client.GetTrackModelsFeaturesAsync(
            new GetTrackModelFeaturesRequest());

        return result.Features;
    }

    public async Task<(decimal minPrice, decimal maxPrice)> GetModelsPriceRange()
    {
        var result = await _client.GetTrackModelPriceRangeAsync(
            new GetTrackModelPriceRangeRequest());

        return ((decimal)result.MinPrice, (decimal)result.MaxPrice);
    }

    public IAsyncEnumerable<GetTrackModelResponse> GetModels(GetTrackModelsRequest request)
    {
        using var response = _client.GetTrackModels(request);
        return response.ResponseStream.ReadAllAsync();
    }
}