using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
}