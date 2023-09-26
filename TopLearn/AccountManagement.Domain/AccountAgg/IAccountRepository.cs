using _0.Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg;

public interface IAccountRepository : IGenericRepository<long, Account>
{
    Task<Account> GetByEmail(string email);
    Task<Account> GetByActiveCode(string activeCode);
    Task<EditProfileCommand> GetAccountForEditProfile(long id);
}