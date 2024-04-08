using _0.Framework.Application;

namespace AccountManagement.Domain.PermissionAgg;

public interface IPermissionRepository
{
    Task AddPermission(AccountRole permission);
    Task<List<int>> GetRolesBy(long accountId);
    Task RemovePermission(long accountId);
    Task Save();
}