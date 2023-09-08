using _0.Framework.Infrastructure;
using AccountManagement.Domain.RoleAgg;

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
}