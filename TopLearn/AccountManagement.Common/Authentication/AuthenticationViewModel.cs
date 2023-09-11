namespace AccountManagement.Common.Authentication;

public class AuthenticationViewModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public bool RememberMe { get; set; }
}