using _0.Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Common.Authentication;
using AccountManagement.Common.Convertors;
using AccountManagement.Common.PasswordHasher;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.PermissionAgg;

namespace AccountManagement.Application;

public class AccountApplication : IAccountApplication
{
    #region constructor injection

    private readonly IAccountRepository _accountRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationHelper _authenticationHelper;
    private readonly IFileUploader _fileUploader;
    public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IAuthenticationHelper authenticationHelper, IFileUploader fileUploader, IPermissionRepository permissionRepository)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _authenticationHelper = authenticationHelper;
        _fileUploader = fileUploader;
        _permissionRepository = permissionRepository;
    }

    #endregion

    public async Task<Tuple<OperationResult, AccountViewModel>> RegisterAccount(AccountRegisterCommand command)
    {
        var email = command.Email.FixEmail();

        if (await _accountRepository.IsExist(x => x.Email.Equals(email)))
        {
            return Tuple.Create(OperationResult.Error("ایمیل وارد شده قبلا در سایت ثبت شده است"), new AccountViewModel());
        }

        if (!command.Password.Equals(command.RePassword))
        {
            return Tuple.Create(OperationResult.Error("کلمه عبور با تکرار آن همخوانی ندارد"), new AccountViewModel());
        }

        var password = _passwordHasher.Hash(command.Password);

        var account = new Account(command.FullName, email, command.Username, password, "", false);
        await _accountRepository.Create(account);
        await _accountRepository.Save();

        return Tuple.Create(OperationResult.Success("ثبت نام شما با موفقیت انجام شد"), new AccountViewModel()
        {
            Email = account.Email,
            ActiveCode = account.ActiveCode,
            FullName = account.FullName
        });
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

    public async Task<OperationResult> ActiveAccount(long id)
    {
        var account = await _accountRepository.Get(id);

        if (account == null)
        {
            return OperationResult.NotFound("کاربری با مشخصات ارسالی یافت نشد");
        }

        if (account.IsActive)
        {
            return OperationResult.Error("حساب از قبل فعال بوده است");
        }

        account.ActivateAccount();
        await _accountRepository.Save();

        return OperationResult.Success("حساب با موفقیت فعال شد");
    }

    public async Task<OperationResult> DeActiveAccount(long id)
    {
        var account = await _accountRepository.Get(id);

        if (account == null)
        {
            return OperationResult.NotFound("کاربری با مشخصات ارسالی یافت نشد");
        }

        if (!account.IsActive)
        {
            return OperationResult.Error("حساب از قبل غیر فعال بوده است");
        }

        account.DeActivateAccount();
        await _accountRepository.Save();

        return OperationResult.Success("حساب با موفقیت غیر فعال شد");
    }

    public Task<OperationResult> LogoutAccount()
    {
        _authenticationHelper.SignOut();

        return Task.FromResult(OperationResult.Success("از سایت خارح شدید"));
    }

    public async Task<Tuple<OperationResult, AccountViewModel>> ForgotPassword(ForgotPasswordCommand command)
    {
        var email = command.Email.FixEmail();
        var account = await _accountRepository.GetByEmail(email);

        if (account == null)
        {
            return Tuple.Create(OperationResult.NotFound("کاربری با مشخصات وارد شده یافت نشد"),
                new AccountViewModel());
        }

        return Tuple.Create(OperationResult.Success("ایمیل بازیابی کلمه عبور برای شما ارسال شد"), new AccountViewModel()
        {
            Email = account.Email,
            ActiveCode = account.ActiveCode,
            FullName = account.FullName
        });
    }

    public async Task<OperationResult> ResetPassword(ResetPasswordCommand command)
    {
        var account = await _accountRepository.GetByActiveCode(command.ActiveCode);

        if (account == null)
        {
            return OperationResult.NotFound("کاربری با مشخصات ارسالی یافت نشد");
        }

        if (!account.IsActive)
        {
            return OperationResult.Error("حساب شما فعال نمیباشد");
        }

        if (!command.Password.Equals(command.RePassword))
        {
            return OperationResult.Error("کلمه عبور با تکرار آن همخوانی ندارد");
        }

        var password = _passwordHasher.Hash(command.Password);

        account.ResetPassword(password);
        await _accountRepository.Save();

        return OperationResult.Success("رمز عبور شما با موفقیت تغییر کرد");
    }

    public async Task<bool> CheckAccountByActiveCode(string activeCode)
    {
        return await _accountRepository.IsExist(x => x.ActiveCode.Equals(activeCode));
    }

    public async Task<OperationResult> EditProfile(EditProfileCommand command)
    {
        var account = await _accountRepository.Get(command.Id);

        var email = command.Email;

        if (account == null)
        {
            return OperationResult.NotFound("کاربری با مشخصات ارسالی یافت نشد");
        }

        if (await _accountRepository.IsExist(x =>
                x.Email == email && x.Id != command.Id))
        {
            return OperationResult.Error("ایمیل وارد شده قبلا در سایت ثبت شده است");
        }

        var imageName = _fileUploader.Upload(command.Image, "UserImages");

        account.EditProfile(command.FullName, command.Email, command.Username, imageName);

        await _accountRepository.Save();

        if (command.Image != null)
        {
            if (!command.ImageName.Equals("no-profile.jpg"))
            {
                _fileUploader.Delete("UserImages", command.ImageName);
            }
        }

        return OperationResult.Success("برای اعمال تغییرات دوباره وارد سایت شوید");
    }

    public async Task<EditProfileCommand> GetAccountForEditProfile(long id)
    {
        return await _accountRepository.GetAccountForEditProfile(id);
    }

    public async Task<OperationResult> ChangePassword(ChangePasswordCommand command)
    {
        var account = await _accountRepository.Get(command.AccountId);

        if (account == null)
        {
            return OperationResult.NotFound("کاربری با مشخصات ارسالی یافت نشد");
        }

        var (verified, _) = _passwordHasher.Check(account.Password, command.OldPassword);

        if (!verified)
        {
            return OperationResult.Error("رمز عبور اشتباه است");
        }

        if (!command.Password.Equals(command.RePassword))
        {
            return OperationResult.Error("کلمه عبور با تکرار آن همخوانی ندارد");
        }

        var password = _passwordHasher.Hash(command.Password);

        account.ChangePassword(password);
        await _accountRepository.Save();

        return OperationResult.Success("برای اعمال تغییرات دوباره وارد سایت شوید");
    }

    public async Task<AccountViewModel> InformationAccount(string email)
    {
        return await _accountRepository.InformationAccount(email);
    }

    public async Task<AccountViewModel> UserPanelSidebar(string email)
    {
        return await _accountRepository.UserPanelSidebar(email);
    }

    public List<AccountViewModel> GetList(AccountSearchModel searchModel)
    {
        return _accountRepository.GetFilteredList(searchModel);
    }

    public async Task<OperationResult> Create(CreateAccountCommand command, List<int> rolesId)
    {
        var email = command.Email.FixEmail();

        if (await _accountRepository.IsExist(x => x.Email.Equals(email)))
        {
            return OperationResult.Error("ایمیل وارد شده قبلا در سایت ثبت شده است");
        }
        
        if (await _accountRepository.IsExist(x => x.Username.Equals(command.Username)))
        {
            return OperationResult.Error("نام کاربری وارد شده قبلا در سایت ثبت شده است");
        }

        var password = _passwordHasher.Hash(command.Password);

        var imageName = _fileUploader.Upload(command.Profile, "UserImages");

        var account = new Account(command.FullName, command.Email, command.Username, password, imageName,
            command.IsActive);

        await _accountRepository.Create(account);
        await _accountRepository.Save();

        foreach (var roleId in rolesId)
        {
            var permission = new AccountRole(account.Id, roleId);
            await _permissionRepository.AddPermission(permission);
            await _permissionRepository.Save();
        }

        return OperationResult.Success("حساب با موفقیت افزوده شد");
    }
}