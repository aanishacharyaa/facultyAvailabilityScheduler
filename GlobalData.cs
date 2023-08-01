public static class GlobalData
{

    public static async Task SetAccessToken(string token)
    {
        await SecureStorage.SetAsync("AccessToken", token);
    }

    public static async Task<string> GetAccessToken()
    {
        return await SecureStorage.GetAsync("AccessToken");
    }
    public static async Task SetUserId(string userId)
    {
        await SecureStorage.SetAsync("UserId", userId);
    }

    public static async Task<string> GetUserId()
    {
        return await SecureStorage.GetAsync("UserId");
    }

    public static async Task SetUserName(string userName)
    {
        await SecureStorage.SetAsync("UserName", userName);
    }

    public static async Task<string> GetUserName()
    {
        return await SecureStorage.GetAsync("UserName");
    }

    public static async Task SetUserEmail(string userEmail)
    {
        await SecureStorage.SetAsync("UserEmail", userEmail);
    }

    public static async Task<string> GetUserEmail()
    {
        return await SecureStorage.GetAsync("UserEmail");
    }
}
