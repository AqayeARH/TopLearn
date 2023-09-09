using _0.Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Common.Convertors;
using AccountManagement.Common.PasswordHasher;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application;

public class AccountApplication : IAccountApplication
{
    #region constructor injection

    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
    }

    #endregion

    public async Task<Tuple<OperationResult, string>> RegisterAccount(AccountRegisterCommand command)
    {
        var email = command.Email.FixEmail();

        if (await _accountRepository.IsExist(x=>x.Email.Equals(email)))
        {
            return Tuple.Create(OperationResult.Error("ایمیل وارد شده قبلا در سایت ثبت شده است"), "");
        }

        if (!command.Password.Equals(command.RePassword))
        {
            return Tuple.Create(OperationResult.Error("کلمه عبور با تکرار آن همخوانی ندارد"), "");
        }

        var password = _passwordHasher.Hash(command.Password);

        var account = new Account(command.FullName, email, command.Username, password, "", false);
        await _accountRepository.Create(account);
        await _accountRepository.Save();

        return Tuple.Create(OperationResult.Success("ثبت نام شما با موفقیت انجام شد"), email);
    }
}