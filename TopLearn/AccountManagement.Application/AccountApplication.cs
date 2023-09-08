using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application;

public class AccountApplication : IAccountApplication
{
    #region constructor injection

    private readonly IAccountRepository _accountRepository;
    public AccountApplication(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    #endregion
}