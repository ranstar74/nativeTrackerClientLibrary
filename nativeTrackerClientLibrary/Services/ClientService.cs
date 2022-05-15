using nativeTrackerClientService;

namespace nativeTrackerClientLibrary.Services;

public class ClientService : ProtoServiceBase
{
    private readonly nativeTrackerClientService.ClientService.ClientServiceClient _client;

    public ClientService()
    {
        _client = new nativeTrackerClientService.ClientService.ClientServiceClient(Channel);
    }

    public async Task<CreateStatus> CreateAccount(CreateAccountRequest request)
    {
        var result = await _client.CreateAccountAsync(request);

        return result.Status;
    }

    public async Task<bool> LoginAccount(string login, string password)
    {
        var response = await _client.LoginAccountAsync(new LoginAccountRequest()
        {
            Login = login,
            Password = password
        });

        CredentialsManager.Token = response.Token;
        return response.Authorized;
    }
}