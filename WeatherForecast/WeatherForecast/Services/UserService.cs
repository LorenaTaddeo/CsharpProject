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

    public void CreateUser(string username, string password)
    {
        if (_dbContext.Users.Any(u => u.Nickname == username))
        {
            throw new Exception("User already exists.");
        }

        var newUser = new User
        {
            Nickname = username,
            Password = password
        };

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }

    public User GetUserById(int id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> ListUsers()
    {
        return _dbContext.Users.ToList();
    }

    public void DeleteUser(int userId)
    {
        var user = _dbContext.Users.Find(userId);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }

    public void ChangePassword(int userId, string newPassword)
    {
        var user = _dbContext.Users.Find(userId);
        if (user != null)
        {
            user.Password = newPassword;
            _dbContext.SaveChanges();
        }
    }
}