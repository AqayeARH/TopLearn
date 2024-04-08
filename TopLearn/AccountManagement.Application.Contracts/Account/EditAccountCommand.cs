namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccountCommand : CreateAccountCommand
    {
        public long Id { get; set; }
        public string ImageName { get; set; }
        public List<int> AccountRoles { get; set; }
    }
}
