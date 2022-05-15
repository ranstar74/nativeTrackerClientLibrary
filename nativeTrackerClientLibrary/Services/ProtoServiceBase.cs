using Grpc.Core;
using Grpc.Net.Client;

namespace nativeTrackerClientLibrary.Services;

public abstract class ProtoServiceBase
{
    protected GrpcChannel Channel { get; }

    protected ProtoServiceBase()
    {
        var credentials = CallCredentials.FromInterceptor((context, metadata) =>
        {
            if (!string.IsNullOrEmpty(CredentialsManager.Token))
            {
                metadata.Add("Authorization", $"Bearer {CredentialsManager.Token}");
            }
            return Task.CompletedTask;
        });
        // SslCredentials is used here because this channel is using TLS.
        // Channels that aren't using TLS should use ChannelCredentials.Insecure instead.
        Channel = GrpcChannel.ForAddress(ServerConfiguration.Address, new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
        });
    }
}