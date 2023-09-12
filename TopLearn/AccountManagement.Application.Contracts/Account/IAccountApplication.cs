using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Account;

public interface IAccountApplication
{
    Task<Tuple<OperationResult, AccountViewModel>> RegisterAccount(AccountRegisterCommand command);
    Task<OperationResult> LoginAccount(LoginAccountCommand command);
    Task<OperationResult> ActiveAccount(string activeCode);
    Task<OperationResult> LogoutAccount();
    Task<Tuple<OperationResult,AccountViewModel>> ForgotPassword(ForgotPasswordCommand command);
    Task<OperationResult> ResetPassword(ResetPasswordCommand command);
    Task<bool> CheckAccountByActiveCode(string activeCode);
}