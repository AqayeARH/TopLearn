namespace AccountManagement.Application.Contracts.Account;

public class AccountViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ActiveCode { get; set; }
    public string ImageName { get; set; }
    public bool IsActive { get; set; }
}