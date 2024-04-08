using AccountManagement.Domain.PermissionAgg;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<int>> GetRolesBy(long accountId)
    {
        return await _context.AccountRoles
            .Where(x => x.AccountId == accountId)
            .Select(x => x.RoleId)
            .ToListAsync();
    }

    public async Task RemovePermission(long accountId)
    {
        var permissions = await _context.AccountRoles
            .Where(x => x.AccountId == accountId)
            .ToListAsync();

        foreach (var permission in permissions)
        {
            _context.AccountRoles.Remove(permission);
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}