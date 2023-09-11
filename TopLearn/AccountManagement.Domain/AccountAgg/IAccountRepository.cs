using _0.Framework.Domain;

namespace AccountManagement.Domain.AccountAgg;

public interface IAccountRepository : IGenericRepository<long, Account>
{
    Task<Account> GetByEmail(string email);
    Task<Account> GetByActiveCode(string activeCode);
}