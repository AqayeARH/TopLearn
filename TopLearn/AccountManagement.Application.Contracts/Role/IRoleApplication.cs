namespace AccountManagement.Application.Contracts.Role;

public interface IRoleApplication
{
    Task<List<RoleViewModel>> GetList();
}