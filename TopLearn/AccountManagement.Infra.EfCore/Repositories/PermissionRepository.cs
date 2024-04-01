using _0.Framework.Application;
using AccountManagement.Domain.PermissionAgg;

namespace AccountManagement.Infra.EfCore.Repositories;

public class PermissionRepository : IPermissionRepository
{
    #region constructor injection

    private readonly AccountManagementContext _context;
    public PermissionRepository(AccountManagementContext context)
    {
        _context = context;
    }

    #endregion
    public async Task AddPermission(AccountRole permission)
    {
        await _context.AccountRoles.AddAsync(permission);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}