using _0.Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.WalletAgg;

public class Wallet : BaseEntity<long>
{
    public int TypeId { get; private set; }
    public long AccountId { get; private set; }
    public double Amount { get; private set; }
    public string Description { get; private set; }
    public bool IsPay { get; private set; }
    public Account Account { get; private set; }
    public WalletType WalletType { get; private set; }

    public Wallet(int typeId, long accountId, double amount, string description)
    {
        TypeId = typeId;
        AccountId = accountId;
        Amount = amount;
        Description = description;
        IsPay = false;
    }
}