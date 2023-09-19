namespace TopLearn.Query.Contracts.UserPanel;

public interface IAccountQuery
{
    Task<AccountQueryModel> InformationAccount(string email);
    Task<AccountQueryModel> UserPanelSidebar(string email);
}