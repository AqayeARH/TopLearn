namespace AccountManagement.Application.Contracts.Wallet
{
    public class WalletViewModel
    {
        public long WalletId { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public bool IsPayed { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string CreationDate { get; set; }
    }
}