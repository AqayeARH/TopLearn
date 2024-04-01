using _0.Framework.Application;

namespace AccountManagement.Domain.PermissionAgg;

public interface IPermissionRepository
{
    Task AddPermission(AccountRole permission);
    Task Save();
}