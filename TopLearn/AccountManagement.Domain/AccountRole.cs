using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain;

public class AccountRole
{
    public long AccountId { get; private set; }
    public int RoleId { get; private set; }
    public Role Role { get; private set; }
    public Account Account { get; private set; }

    public AccountRole(long accountId, int roleId)
    {
        AccountId = accountId;
        RoleId = roleId;
    }
}