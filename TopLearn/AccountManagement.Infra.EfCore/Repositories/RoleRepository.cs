using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infra.EfCore.Repositories;

public class RoleRepository : EfCoreGenericRepository<int, Role>, IRoleRepository
{
    #region constructor injection

    private readonly AccountManagementContext _context;
    public RoleRepository(AccountManagementContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<List<RoleViewModel>> GetList()
    {
        return await _context.Roles.OrderBy(x => x.Id)
            .Select(x => new RoleViewModel()
            {
                Id = x.Id,
                RoleTitle = x.RoleTitle
            }).ToListAsync();
    }
}