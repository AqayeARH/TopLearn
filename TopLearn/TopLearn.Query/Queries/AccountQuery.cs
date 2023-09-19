using _0.Framework.Application;
using AccountManagement.Infra.EfCore;
using Microsoft.EntityFrameworkCore;
using TopLearn.Query.Contracts.UserPanel;

namespace TopLearn.Query.Queries;

public class AccountQuery : IAccountQuery
{
    #region constractor injection

    private readonly AccountManagementContext _accountContext;
    public AccountQuery(AccountManagementContext accountContext)
    {
        _accountContext = accountContext;
    }

    #endregion
    public async Task<AccountQueryModel> InformationAccount(string email)
    {
        var account = await _accountContext.Accounts
            .Select(x => new AccountQueryModel()
            {
                Email = x.Email,
                FullName = x.FullName,
                RegisterDate = x.CreationDate.ToFarsi(),
                Username = x.Username,
                Wallet = 0
            })
            .SingleOrDefaultAsync(x => x.Email.Equals(email));

        return account;
    }

    public async Task<AccountQueryModel> UserPanelSidebar(string email)
    {
        var account = await _accountContext.Accounts
            .Select(x => new AccountQueryModel()
            {
                Email = x.Email,
                FullName = x.FullName,
                ImageName = x.ImageName,
                RegisterDate = x.CreationDate.ToFarsi(),
            })
            .SingleOrDefaultAsync(x => x.Email.Equals(email));

        return account;
    }
}