using Microsoft.Extensions.Configuration;

public class AuthService
{
private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool ValidateAdminCredentials(string username, string password)
    {
        var adminUsername = _configuration["AdminUser:Nickname"];
        var adminPassword = _configuration["AdminUser:Password"];

        return username == adminUsername && password == adminPassword;
    }
}
