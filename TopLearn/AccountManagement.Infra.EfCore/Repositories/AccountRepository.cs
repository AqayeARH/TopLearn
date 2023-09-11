using _0.Framework.Infrastructure;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Account> GetByEmail(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<Account> GetByActiveCode(string activeCode)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.ActiveCode.Equals(activeCode));
    }
}