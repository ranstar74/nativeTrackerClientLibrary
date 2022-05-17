using nativeTrackerClientLibrary.Services;

namespace nativeTrackerClientLibrary;

public static class CredentialsManager
{
    public static string Token { get; set; } = string.Empty;

    private static readonly ClientService ClientService = new();
    public static async Task<bool> IsAuthorized()
    {
        return await ClientService.ValidateTokenAsync(Token);
    }
}