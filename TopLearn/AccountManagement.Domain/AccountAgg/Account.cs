using _0.Framework.Domain;
using AccountManagement.Domain.PermissionAgg;
using AccountManagement.Domain.WalletAgg;

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
    public List<Wallet> Wallets { get; private set; }

    //Constructor
    public Account(string fullName, string email, string username, string password, string imageName, bool isActive)
    {
        FullName = fullName;
        Email = email;
        Username = username;
        Password = password;
        ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
        ImageName = string.IsNullOrEmpty(imageName) ? "no-profile.jpg" : imageName;
        IsActive = isActive;
        AccountRoles = new List<AccountRole>();
        Wallets = new List<Wallet>();
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

    public void ResetPassword(string password)
    {
        Password = password;
        ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
    }

    public void EditProfile(string fullName, string email, string username, string imageName)
    {
        FullName = fullName;
        Email = email;
        Username = username;
        if (!string.IsNullOrEmpty(imageName))
        {
            ImageName = imageName;
        }
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }

    public void Edit(string fullName, string email, string username, string imageName, bool isActive)
    {
        FullName = fullName;
        Email = email;
        Username = username;
        if (!string.IsNullOrEmpty(imageName))
        {
            ImageName = imageName;
        }
        IsActive = isActive;
    }
}