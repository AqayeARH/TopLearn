using _0.Framework.Application;
using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.WalletAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infra.EfCore.Repositories;

public class AccountRepository : EfCoreGenericRepository<long, Account>, IAccountRepository
{
    #region constructor injection

    private readonly AccountManagementContext _context;

    public AccountRepository(AccountManagementContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task<Account> GetByEmail(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<Account> GetByActiveCode(string activeCode)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.ActiveCode.Equals(activeCode));
    }

    public async Task<EditProfileCommand> GetAccountForEditProfile(long id)
    {
        return await _context.Accounts.Select(x => new EditProfileCommand()
        {
            Email = x.Email,
            FullName = x.FullName,
            Id = x.Id,
            Username = x.Username,
            ImageName = x.ImageName
        }).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AccountViewModel> InformationAccount(string email)
    {
        var account = await _context.Accounts
            .Select(x => new AccountViewModel()
            {
                Email = x.Email,
                FullName = x.FullName,
                RegisterDate = x.CreationDate.ToFarsi(),
                Username = x.Username,
                Id = x.Id,
            })
            .SingleOrDefaultAsync(x => x.Email.Equals(email));

        var wallets = await _context.Wallets
            .Where(x => x.AccountId == account.Id)
            .Select(x => new { x.Amount, x.IsPay, x.TypeId })
            .ToListAsync();

        var balancedAmount = wallets
            .Where(x => x.IsPay && x.TypeId == WalletTypeId.In)
            .Sum(x => x.Amount) - wallets
            .Where(x => x.IsPay && x.TypeId == WalletTypeId.Out)
            .Sum(x => x.Amount);

        if (account != null)
        {
            account.Wallet = balancedAmount >= 0 ? balancedAmount : 0;
        }

        return account;
    }

    public async Task<AccountViewModel> UserPanelSidebar(string email)
    {
        var account = await _context.Accounts
            .Select(x => new AccountViewModel()
            {
                Email = x.Email,
                FullName = x.FullName,
                ImageName = x.ImageName,
                RegisterDate = x.CreationDate.ToFarsi(),
            })
            .SingleOrDefaultAsync(x => x.Email.Equals(email));

        return account;
    }

    public List<AccountViewModel> GetFilteredList(AccountSearchModel searchModel)
    {
        var accounts =  _context.Accounts
            .Select(x => new AccountViewModel()
            {
                Email = x.Email,
                FullName = x.FullName,
                Id = x.Id,
                ImageName = x.ImageName,
                RegisterDate = x.CreationDate.ToFarsi(),
                Username = x.Username,
                IsActive = x.IsActive
            }).AsQueryable();

        #region Filtering

        if (!string.IsNullOrEmpty(searchModel.Email))
        {
            accounts = accounts.Where(x => x.Email.Contains(searchModel.Email));
        }

        if (!string.IsNullOrEmpty(searchModel.FullName))
        {
            accounts = accounts.Where(x => x.FullName.Contains(searchModel.FullName));
        }

        if (searchModel.NotActivatedAccount)
        {
            accounts = accounts.Where(x => x.IsActive == false);
        }

        #endregion

        return accounts.ToList();
    }
}