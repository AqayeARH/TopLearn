using _0.Framework.Infrastructure;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Infra.EfCore.Repositories;

public class AccountRepository : EfCoreGenericRepository<long, Account>, IAccountRepository
{
    #region constructor injection

    private readonly AccountManagementContext _context;

    public AccountRepository(AccountManagementContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}