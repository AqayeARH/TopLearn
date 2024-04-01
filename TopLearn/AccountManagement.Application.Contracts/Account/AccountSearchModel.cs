namespace AccountManagement.Application.Contracts.Account;

public class AccountSearchModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool NotActivatedAccount { get; set; }
}