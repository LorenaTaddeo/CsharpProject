using WeatherApp.Data;

public class UserService
{
    private readonly WeatherDbContext _dbContext;

    public UserService(WeatherDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Authenticate(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == username && u.Password == password);
        return user != null;
    }

    public bool IsAdmin(string username)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Nickname == username);
        return user != null && user.Nickname == "admin";
    }
}