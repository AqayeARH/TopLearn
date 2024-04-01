using AccountManagement.Domain.PermissionAgg;

namespace AccountManagement.Domain.RoleAgg;

public class Role
{
    public int Id { get; private set; }
    public string RoleTitle { get; private set; }
    public List<AccountRole> AccountRoles { get; private set; }

    public Role(int id, string roleTitle)
    {
        Id = id;
        RoleTitle = roleTitle;
        AccountRoles = new List<AccountRole>();
    }
}