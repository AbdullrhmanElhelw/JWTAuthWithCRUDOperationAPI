namespace JWTAuthorization.BL;

public class LoginDTO : ILogging
{
    public string Email { get; set ; }
    public string Password { get; set; }
}
