using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Account;

public interface IAccountApplication
{
    Task<Tuple<OperationResult, string>> RegisterAccount(AccountRegisterCommand command);
}