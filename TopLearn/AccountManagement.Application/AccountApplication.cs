using _0.Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Common.Authentication;
using AccountManagement.Common.Convertors;
using AccountManagement.Common.PasswordHasher;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application;

public class AccountApplication : IAccountApplication
{
    #region constructor injection

    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationHelper _authenticationHelper;
    public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IAuthenticationHelper authenticationHelper)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _authenticationHelper = authenticationHelper;
    }

    #endregion

    public async Task<Tuple<OperationResult, string>> RegisterAccount(AccountRegisterCommand command)
    {
        var email = command.Email.FixEmail();

        if (await _accountRepository.IsExist(x => x.Email.Equals(email)))
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

    public async Task<OperationResult> LoginAccount(LoginAccountCommand command)
    {
        var email = command.Email.FixEmail();

        var account = await _accountRepository.GetByEmail(email);

        if (account == null)
        {
            return OperationResult.Error("کاربری با مشخصات ارسال شده یافت نشد");
        }

        var (verified, _) = _passwordHasher.Check(account.Password, command.Password);

        if (!verified)
        {
            return OperationResult.Error("کاربری با مشخصات ارسال شده یافت نشد");
        }

        if (!account.IsActive)
        {
            return OperationResult.Error("حساب کاربری شما فعال نیست");
        }

        _authenticationHelper.Signin(new AuthenticationViewModel()
        {
            Email = account.Email,
            Username = account.Username,
            Fullname = account.FullName,
            Id = account.Id,
            RememberMe = command.RememberMe
        });

        return OperationResult.Success("با موفقیت وارد سایت شدید");
    }

    public async Task<OperationResult> ActiveAccount(string activeCode)
    {
        var account = await _accountRepository.GetByActiveCode(activeCode);

        if (account == null)
        {
            return OperationResult.NotFound("کاربری با مشخصات ارسالی یافت نشد");
        }

        if (account.IsActive)
        {
            return OperationResult.Error("حساب شما از قبل فعال بوده است");
        }

        account.ActivateAccount();
        await _accountRepository.Save();

        return OperationResult.Success("حساب با موفقیت فعال شد");
    }
}