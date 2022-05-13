using Grpc.Net.Client;

namespace nativeTrackerClientLibrary;

public abstract class ProtoServiceBase
{
    protected GrpcChannel Channel { get; }

    protected ProtoServiceBase()
    {
        Channel = GrpcChannel.ForAddress("https://localhost:7225");
    }
}