using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Account;

public interface IAccountApplication
{
    Task<Tuple<OperationResult, AccountViewModel>> RegisterAccount(AccountRegisterCommand command);
    Task<OperationResult> LoginAccount(LoginAccountCommand command);
    Task<OperationResult> ActiveAccount(string activeCode);
    Task<OperationResult> ActiveAccount(long id);
    Task<OperationResult> DeActiveAccount(long id);
    Task<OperationResult> LogoutAccount();
    Task<Tuple<OperationResult, AccountViewModel>> ForgotPassword(ForgotPasswordCommand command);
    Task<OperationResult> ResetPassword(ResetPasswordCommand command);
    Task<bool> CheckAccountByActiveCode(string activeCode);
    Task<OperationResult> EditProfile(EditProfileCommand command);
    Task<EditProfileCommand> GetAccountForEditProfile(long id);
    Task<OperationResult> ChangePassword(ChangePasswordCommand command);
    Task<AccountViewModel> InformationAccount(string email);
    Task<AccountViewModel> UserPanelSidebar(string email);
    List<AccountViewModel> GetList(AccountSearchModel searchModel);
    Task<OperationResult> Create(CreateAccountCommand command,List<int> rolesId);
}