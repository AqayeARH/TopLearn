using _0.Framework.Domain;

namespace AccountManagement.Domain.AccountAgg;

public class Account : BaseEntity<long>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string ActiveCode { get; private set; }
    public string ImageName { get; private set; }
    public bool IsActive { get; private set; }
    public List<AccountRole> AccountRoles { get; private set; }

    //Constructor
    public Account(string fullName, string email, string username, string password, string imageName, bool isActive)
    {
        FullName = fullName;
        Email = email;
        Username = username;
        Password = password;
        ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
        ImageName = !string.IsNullOrEmpty(imageName) ? imageName : "no-profile";
        IsActive = isActive;
        AccountRoles = new List<AccountRole>();
    }

    //Methods
    public void ActivateAccount()
    {
        IsActive = true;
        ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
    }

    public void DeActivateAccount()
    {
        IsActive = false;
    }
}