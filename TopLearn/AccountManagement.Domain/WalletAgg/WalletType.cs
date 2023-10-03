namespace AccountManagement.Domain.WalletAgg;

public class WalletType
{
    public int TypeId { get; private set; }
    public string TypeTitle { get; private set; }
    public List<Wallet> Wallets { get; private set; }

    public WalletType(int typeId)
    {
        TypeId = typeId;
        TypeTitle = typeId switch
        {
            WalletTypeId.In => "واریز",
            WalletTypeId.Out => "برداشت",
            _ => "خطا"
        };
        Wallets = new List<Wallet>();
    }
}