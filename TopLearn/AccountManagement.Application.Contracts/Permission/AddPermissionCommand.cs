namespace AccountManagement.Application.Contracts.Permission;

public class AddPermissionCommand
{
    public long AccountId { get; set; }
    public List<int> RolesId { get; set; }
}